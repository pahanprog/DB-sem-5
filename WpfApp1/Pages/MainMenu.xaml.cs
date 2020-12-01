using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using WpfApp1.Models;
using WpfApp1.Operations;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        SqlDataAdapter adapter;
        DataTable searchTable;

        string ignoreSearch = "";

        int searchBoxSelectedIndex = 0;

        SearchOperations searchOp = new SearchOperations();

        public MainMenu()
        {
            InitializeComponent();
        }


        int minvalue,
        maxvalue,
        startvalue;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (Globals.LoggedInUser.Status == "Admin")
            {
                AdminMenuBtn.Visibility = Visibility.Visible;
            }

            try
            {
                SqlConnection con = new SqlConnection(Globals.DBurl);
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT MAX(Year) as max, MIN(Year) as min FROM  Model", con);
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    startvalue = dr.GetInt32(1);
                    maxvalue = dr.GetInt32(0);
                    minvalue = dr.GetInt32(1);
                }

                con.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            NUDTextBox.Text = minvalue.ToString();
            NUDTextBox1.Text = maxvalue.ToString();
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            searchBoxSelectedIndex = 0;
            keywordHelper.Visibility = Visibility.Collapsed;
            keywordHelper.ItemsSource = null;

            string search = SearchBox.Text;
            if (search.Length <= 0)
            {
                return;
            }
            search = search.Trim();
            string[] searchKeys = search.Split(" ");

            if (searchKeys.Length > 1)
            {
                for (int i = 0; i < searchKeys.Length; i++)
                {
                    if (i != searchKeys.Length-1)
                    {
                        searchKeys[i] = searchKeys[i].Insert(searchKeys[i].Length, "*\" OR");
                        searchKeys[i] = searchKeys[i].Insert(0, "\"*");
                    } else
                    {
                        searchKeys[i] = searchKeys[i].Insert(searchKeys[i].Length, "*\"");
                        searchKeys[i] = searchKeys[i].Insert(0, "\"*");
                    }
                }
            } else
            {
                searchKeys[0] = searchKeys[0].Insert(searchKeys[0].Length, "*\"");
                searchKeys[0] = searchKeys[0].Insert(0, "\"*");
            }

            string sqlSearch = String.Join(" ", searchKeys);

            string sql = "SELECT search.Id,search.Name,search.CarBody,search.Year,search.Seats,search.EngineType,search.BrandName, COUNT(*) as occurance FROM (",
                sqlEnd = ") AS search GROUP BY Id,Name,CarBody,BrandName,Year,Seats,EngineType ORDER BY occurance DESC";

            string sqlBody = $"Select Model.*,Brand.BrandName FROM Model JOIN Brand ON(Brand.id = BrandId)  WHERE CONTAINS(CarBody, '{sqlSearch}')",
                sqlName = $"Select Model.*,Brand.BrandName FROM Model JOIN Brand ON(Brand.id = BrandId)  WHERE CONTAINS(Name, '{sqlSearch}')",
                sqlBrand = $"Select Model.*,Brand.BrandName FROM Model JOIN Brand ON(Brand.id = BrandId) WHERE CONTAINS(BrandName, '{sqlSearch}')";

            string[] sqlSelections = { sqlBody, sqlName, sqlBrand };

            string firstWord = "AND";


            if (search.Trim()[0] == '/')
            {
                if (search.Trim() == "/all")
                {
                    sqlBody = $"Select Model.*,Brand.BrandName FROM Model JOIN Brand ON(Brand.id = BrandId)";
                    sqlName = $"Select Model.*,Brand.BrandName FROM Model JOIN Brand ON(Brand.id = BrandId)";
                    sqlBrand = $"Select Model.*,Brand.BrandName FROM Model JOIN Brand ON(Brand.id = BrandId)";

                    firstWord = "WHERE";

                    sqlSelections = new string[] { sqlBody, sqlName, sqlBrand };
                }
                else
                {
                    MessageBox.Show("wrong or incomplete command");
                }
            }
                if (BodyFilter.SelectedIndex != -1)
                {
                    if (EngineFilter.SelectedIndex != -1)
                    {
                        for (int i = 0; i < sqlSelections.Length; i++)
                        {
                            sqlSelections[i] += $" {firstWord} CarBody = '{((KeyValuePair<int, string>)BodyFilter.SelectedItem).Value}' AND EngineType='{((KeyValuePair<int, string>)EngineFilter.SelectedItem).Value}'";
                        }
                    firstWord = "AND";
                }
                    else
                    {
                        for (int i = 0; i < sqlSelections.Length; i++)
                        {
                            sqlSelections[i] += $" {firstWord} CarBody = '{((KeyValuePair<int, string>)BodyFilter.SelectedItem).Value}'";
                        }
                    firstWord = "AND";
                }
                }
                else if (EngineFilter.SelectedIndex != -1)
                {
                    for (int i = 0; i < sqlSelections.Length; i++)
                    {
                        sqlSelections[i] += $" {firstWord} EngineType='{((KeyValuePair<int, string>)EngineFilter.SelectedItem).Value}'";
                    }
                firstWord = "AND";
            }

                if (minYearSwitch.IsChecked.Value)
                {
                    if (maxYearSwitch.IsChecked.Value)
                    {
                        for (int i = 0; i < sqlSelections.Length; i++)
                        {
                            sqlSelections[i] += $" {firstWord} year BETWEEN {NUDTextBox.Text} AND {NUDTextBox1.Text}";
                        }
                    firstWord = "AND";
                }
                    else
                    {
                        for (int i = 0; i < sqlSelections.Length; i++)
                        {
                            sqlSelections[i] += $" {firstWord} year > {NUDTextBox.Text}";
                        }
                    firstWord = "AND";
                }
                }
                else if (maxYearSwitch.IsChecked.Value)
                {
                    for (int i = 0; i < sqlSelections.Length; i++)
                    {
                        sqlSelections[i] += $" {firstWord} year < {NUDTextBox1.Text}";
                    }
                firstWord = "AND";
            }

            sql += String.Join(" UNION ALL ", sqlSelections) + sqlEnd;
            Clipboard.SetText(sql);

            searchTable = new DataTable();
            SqlConnection connection = null;

            try
            {

                connection = new SqlConnection(Globals.DBurl);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);

                connection.Open();
                adapter.Fill(searchTable);
                ListView.ItemsSource = searchTable.DefaultView;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

            }
            finally
            {

                search = null;
                searchKeys = null;
                sqlSearch = null;

            }

        }

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (SearchBox.Text == "Search...")
            {
                SearchBox.Text = "";
            }
        }

        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(SearchBox.Text))
            {
                SearchBox.Text = "Search...";
                keywordHelper.Visibility = Visibility.Collapsed;
                keywordHelper.ItemsSource = null;
            }
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            Car car = new Car();

            ListBoxItem item = sender as ListBoxItem;
            if (item != null && item.IsSelected)
            {
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(item);
                DataTemplate DataTemplate = myContentPresenter.ContentTemplate;

                TextBlock tb = (TextBlock)DataTemplate.FindName("id", myContentPresenter);

                car.Id = Convert.ToInt32(tb.Text);

                NavigationService.Navigate(new CarOnfoById(car));
            }
        }

        private void SelectMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SearchBySelect());
        }

        private async void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text == "")
            {
                ignoreSearch = " ";
                keywordHelper.Visibility = Visibility.Collapsed;
                keywordHelper.ItemsSource = null;
                return;
            }
            if (SearchBox.Text.Length > 0)
            {
                if (SearchBox.Text.Trim()[0] == '/')
                {
                    ObservableCollection<SearchHelper> commandHelpers = new ObservableCollection<SearchHelper>();
                    SearchHelper helper = new SearchHelper()
                    {
                        Helper = "all",
                        Type = "command"
                    };
                    ignoreSearch = "/";
                    commandHelpers.Add(helper);
                    keywordHelper.Visibility = Visibility.Visible;
                    keywordHelper.ItemsSource = commandHelpers;
                    keywordHelper.SelectedIndex = 0;
                    return;
                }
            }

            TextBox tb = (TextBox)sender;
            int startLength = tb.Text.Length;

            await Task.Delay(300);
            if (startLength == tb.Text.Length)
            {

                string search = tb.Text;
                search = search.Trim();
                string[] searchKeys = search.Split(ignoreSearch);

                if (searchKeys.Length > 1)
                {
                    for (int i = 0; i < searchKeys.Length; i++)
                    {
                        if (i != searchKeys.Length - 1)
                        {
                            searchKeys[i] = searchKeys[i].Insert(searchKeys[i].Length, "*\" OR");
                            searchKeys[i] = searchKeys[i].Insert(0, "\"*");
                        }
                        else
                        {
                            searchKeys[i] = searchKeys[i].Insert(searchKeys[i].Length, "*\"");
                            searchKeys[i] = searchKeys[i].Insert(0, "\"*");
                        }
                    }
                }
                else
                {
                    searchKeys[0] = searchKeys[0].Insert(searchKeys[0].Length, "*\"");
                    searchKeys[0] = searchKeys[0].Insert(0, "\"*");
                }

                string sqlSearch = String.Join(" ", searchKeys);

                ObservableCollection<SearchHelper> helpers = searchOp.getSearchHelper(sqlSearch);
                if (helpers.Count != 0)
                {
                    keywordHelper.Visibility = Visibility.Visible;
                    keywordHelper.ItemsSource = helpers;
                    keywordHelper.SelectedIndex = 0;
                }
                else
                {
                    keywordHelper.Visibility = Visibility.Collapsed;
                    keywordHelper.ItemsSource = null;
                    searchBoxSelectedIndex = 0;
                }
            }
            if (startLength > tb.Text.Length && ignoreSearch != " ")
            {
                string[] ignores = ignoreSearch.Split(' ');
                List<string> result = new List<string>();
                foreach(string word in ignores)
                {
                    if (tb.Text.Contains(word))
                    {
                        result.Add(word);
                    }
                }
                ignoreSearch = String.Join(" ",result);
            }
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                SearchHelper helper = (SearchHelper)keywordHelper.SelectedItem;
                if (helper != null)
                {
                    if (ignoreSearch != " ")
                    {
                        if (ignoreSearch == "/")
                        {
                            SearchBox.Text = $"{ignoreSearch}{helper.Helper}";
                            ignoreSearch = $"{ignoreSearch}{helper.Helper}";

                        }
                        else
                        {
                            SearchBox.Text = $"{ignoreSearch} {helper.Helper}";
                            ignoreSearch = $"{ignoreSearch} {helper.Helper}";
                        }
                    }
                    else
                    {
                        SearchBox.Text = helper.Helper;
                        ignoreSearch = $"{helper.Helper}";
                    }
                    SearchBox.CaretIndex = SearchBox.Text.Length;
                }

                searchBoxSelectedIndex = 0;
            }
            if (e.Key == Key.Return)
            {
                e.Handled = true;
                ButtonAutomationPeer peer = new ButtonAutomationPeer(SearchBtn);
                IInvokeProvider invokeProv = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                invokeProv.Invoke();
            }
            if (e.Key == Key.Down)
            {
                if (searchBoxSelectedIndex != keywordHelper.Items.Count - 1)
                {
                    searchBoxSelectedIndex++;
                    keywordHelper.SelectedIndex = searchBoxSelectedIndex;
                }
            } 
            if (e.Key == Key.Up)
            {
                if (searchBoxSelectedIndex > -1)
                {
                    searchBoxSelectedIndex--;
                    keywordHelper.SelectedIndex = searchBoxSelectedIndex;
                }
            }
        }

        private void resetBtn_Click(object sender, RoutedEventArgs e)
        {
            BodyFilter.SelectedIndex = -1;
            EngineFilter.SelectedIndex = -1;
            maxYearSwitch.IsChecked = false;
            minYearSwitch.IsChecked = false;
            NUDTextBox.Text = minvalue.ToString();
            NUDTextBox1.Text = maxvalue.ToString();
        }

        private void AdminMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdminMenu());
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }






        private void NUDButtonUP_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox.Text != "") number = Convert.ToInt32(NUDTextBox.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox.Text = Convert.ToString(number - 1);
        }

        private void NUDTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                NUDButtonUP.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { true });
            }
        }

        private void NUDTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown, new object[] { false });
        }

        private void NUDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox.Text != "")
            if (!int.TryParse(NUDTextBox.Text, out number)) NUDTextBox.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox.Text = minvalue.ToString();
            NUDTextBox.SelectionStart = NUDTextBox.Text.Length;

        }





        private void NUDTextBox1_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                NUDButtonUP1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP1, new object[] { true });
            }


            if (e.Key == Key.Down)
            {
                NUDButtonDown1.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown1, new object[] { true });
            }
        }

        private void NUDTextBox1_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonUP1, new object[] { false });

            if (e.Key == Key.Down)
                typeof(Button).GetMethod("set_IsPressed", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(NUDButtonDown1, new object[] { false });
        }

        private void NUDTextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            int number = 0;
            if (NUDTextBox1.Text != "")
                if (!int.TryParse(NUDTextBox1.Text, out number)) NUDTextBox1.Text = startvalue.ToString();
            if (number > maxvalue) NUDTextBox1.Text = maxvalue.ToString();
            if (number < minvalue) NUDTextBox1.Text = minvalue.ToString();
            NUDTextBox1.SelectionStart = NUDTextBox1.Text.Length;
        }

        private void NUDButtonUP1_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox1.Text != "") number = Convert.ToInt32(NUDTextBox1.Text);
            else number = 0;
            if (number < maxvalue)
                NUDTextBox1.Text = Convert.ToString(number + 1);
        }

        private void NUDButtonDown1_Click(object sender, RoutedEventArgs e)
        {
            int number;
            if (NUDTextBox1.Text != "") number = Convert.ToInt32(NUDTextBox1.Text);
            else number = 0;
            if (number > minvalue)
                NUDTextBox1.Text = Convert.ToString(number - 1);
        }
    }
}
