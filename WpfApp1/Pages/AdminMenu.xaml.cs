using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp1.Models;
using System.Collections.ObjectModel;
using WpfApp1.Operations;
using Microsoft.Win32;
using System.Windows.Media;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls.Primitives;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Page
    {

        ObservableCollection<Car> CarCollection;
        ObservableCollection<Brand> BrandCollection;
        ObservableCollection<User> UserCollection;
        ObservableCollection<Country> CountryCollection;

        AdminMenuOperations admOp = new AdminMenuOperations();

        public enum UserStatus { User, Admin };

        public AdminMenu()
        {
            InitializeComponent();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }   

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserCollection = admOp.initializeUserData();
            CarCollection = admOp.initializeCarData();
            BrandCollection = admOp.initializeBrandData();
            CountryCollection = admOp.initializeCountryData();

            BrandDataGrid.ItemsSource = BrandCollection;
            UserDataGrid.ItemsSource = UserCollection;
            CarDataGrid.ItemsSource = CarCollection;
            CountryDataGrid.ItemsSource = CountryCollection;
        }

        //Car Btn Actions

        private void CarEditBtn_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;

            foreach (Car car in CarDataGrid.Items)
            {
                if (car.Changed)
                {
                    bool queryResult = admOp.saveCarChanges(car);
                    if (!queryResult)
                    {
                        MessageBox.Show($"Failed to save car {car.Id}");
                        result = false;
                    }
                }
            }

            if (result)
            {
                MessageBox.Show("Successfully updated car info");
                CarCollection = admOp.initializeCarData();
                try
                {
                    CarDataGrid.ItemsSource = CarCollection;
                } catch (Exception ex)
                {
                }
            }
        }

        private void CarDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Car car = (Car)CarDataGrid.SelectedItem;

            string messageBoxText = $"Are you sure you want to delete car with id: {car.Id}";
            string caption = "Delete dialog box";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                bool queryResult = admOp.deleteCarFromDataBase(car);

                if (queryResult)
                {
                    CarCollection.Remove(car);
                }
                else
                {
                    MessageBox.Show("Failed to delete car info");
                }
            }
        }

        private void AddCarBtn_Click(object sender, RoutedEventArgs e)
        {
            string packUri = "pack://application:,,,/WpfApp1;component/img/noImage.png";

            Car car = new Car()
            {
                Changed = true,
                Image = new BitmapImage(new Uri(packUri))
            };

            CarCollection.Add(car);
        }

        //Car Toggle visibility

        private void MinusCarBtn_Click(object sender, RoutedEventArgs e)
        {
            CarDataGrid.Visibility = Visibility.Collapsed;
            AddCarBtn.Visibility = Visibility.Collapsed;
            CarEditBtn.Visibility = Visibility.Collapsed;
        }

        private void PlusCarBtn_Click(object sender, RoutedEventArgs e)
        {
            CarDataGrid.Visibility = Visibility.Visible;
            AddCarBtn.Visibility = Visibility.Visible;
            CarEditBtn.Visibility = Visibility.Visible;
        }

        //User Btn Actions

        private void UserDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            User user = (User)UserDataGrid.SelectedItem;

            if (user != null)
            {
                user.Changed = true;
            }
        }

        private void UserEditBtn_Click(object sender, RoutedEventArgs e)
        {
            bool changedLoggedUser = false;

            foreach (User user in UserDataGrid.Items)
            {
                bool queryResult = false;
                if (user.Changed)
                {
                    if (user.Id == Globals.LoggedInUser.Id && !changedLoggedUser)
                    {
                        if (user.StatusId != Globals.LoggedInUser.StatusId)
                        {

                            string statusName = null;
                            if (user.StatusId == 1)
                                statusName = "User";
                            else if (user.StatusId == 0)
                                statusName = "Admin";

                            string messageBoxText = $"Are u sure you want to change your user status to {statusName}, if u click yes you will be logged out and will need to sign in again";
                            string caption = "User status change";
                            MessageBoxButton button = MessageBoxButton.YesNoCancel;
                            MessageBoxImage icon = MessageBoxImage.Warning;
                            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
                            if (result == MessageBoxResult.Yes)
                            {
                                queryResult = saveUserChanges(user);
                                if (!queryResult)
                                    MessageBox.Show($"Failed to save changes in your user");
                                changedLoggedUser = true;
                            }
                            else
                            {
                                user.StatusId = 0;
                                queryResult = saveUserChanges(user);
                                if (!queryResult)
                                    MessageBox.Show($"Failed to save changes in your user");
                                continue;
                            }
                        } else
                        {
                            queryResult = saveUserChanges(user);
                            if (!queryResult)
                                MessageBox.Show($"Failed to save changes in your user");
                        }
                    }
                    queryResult = saveUserChanges(user);
                    if (!queryResult)
                    {
                        MessageBox.Show($"Failed to sasve changes in user {user.Id}");
                        return;
                    }
                }
            }
            UserCollection = admOp.initializeUserData();
            UserDataGrid.ItemsSource = UserCollection;
            MessageBox.Show("Successfully updated users info");
            if (changedLoggedUser)
            {
                Globals.LoggedInUser = null;
                NavigationService.Navigate(new LoginPage());
            }
        }

        private bool saveUserChanges(User user,bool changeStatus = true)
        {
            bool queryResult = admOp.saveUserChanges(user, changeStatus);
            return queryResult;
        }

        private void UserDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)UserDataGrid.SelectedItem;

            string messageBoxText = $"Are you sure you want to delete user with id:  {user.Id}";
            string caption = "Delete dialog box";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);
            if (result == MessageBoxResult.Yes)
            {
                bool queryResult = admOp.deleteUserFromDataBasse(user);

                if (queryResult)
                {
                    UserCollection.Remove(user);
                }
                else
                {
                    MessageBox.Show("Failed to delete user info");
                }
            }
        }

        //User Toggle visibility

        private void PlusUserBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDataGrid.Visibility = Visibility.Visible;
            UserEditBtn.Visibility = Visibility.Visible;
        }

        private void MinusUserBtn_Click(object sender, RoutedEventArgs e)
        {
            UserDataGrid.Visibility = Visibility.Collapsed;
            UserEditBtn.Visibility = Visibility.Collapsed;
        }

        //Brand Btn Actions

        private void BrandEditBtn_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;

            foreach (Brand brand in BrandDataGrid.Items)
            {
                if (brand.Changed)
                {
                    bool queryResult = admOp.saveBrandChanges(brand);

                    if (!queryResult)
                    {
                        MessageBox.Show($"Failed to save brand {brand.Id}");
                        result = false;
                    }
                }
            }

            if (result)
            {
                BrandCollection = admOp.initializeBrandData();
                BrandDataGrid.ItemsSource = BrandCollection;

                MessageBox.Show("Successfully updated brand info");

                NavigationService.Refresh();
            }
        }

        private void BrandDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Brand brand = (Brand)BrandDataGrid.SelectedItem;

            string messageBoxText = $"Are you sure you want to delete brand with id:  {brand.Id}";
            string caption = "Delete dialog box";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                bool queryResult = admOp.deleteBrandFromDataBase(brand);

                if (queryResult)
                {
                    BrandCollection.Remove(brand);

                    CarCollection = admOp.initializeCarData();
                    CarDataGrid.ItemsSource = CarCollection;
                }
                else
                {
                    MessageBox.Show("Failed to delete brand info");
                }
            }
        }

        private void BrandDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Brand brand = (Brand)BrandDataGrid.SelectedItem;

            if (brand != null)
            {
                brand.Changed = true;
            }
        }

        private void CarDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Car car = (Car)CarDataGrid.SelectedItem;

            if (CarDataGrid.CurrentCell.Column.Header != CarDataGrid.Columns[CarDataGrid.Columns.Count -2].Header && car != null)
            {
                car.Changed = true;
            }
        }

        //Brand Toggle visibility

        private void BrandPlusBtn_Click(object sender, RoutedEventArgs e)
        {
            BrandDataGrid.Visibility = Visibility.Visible;
            BrandEditBtn.Visibility = Visibility.Visible;
            AddBrandBtn.Visibility = Visibility.Visible;
        }

        private void BrandMinusBtn_Click(object sender, RoutedEventArgs e)
        {
            BrandDataGrid.Visibility = Visibility.Collapsed;
            BrandEditBtn.Visibility = Visibility.Collapsed;
            AddBrandBtn.Visibility = Visibility.Collapsed;
        }

        private void AddBrandBtn_Click(object sender, RoutedEventArgs e)
        {
            Brand brand = new Brand()
            {
                Changed = true
            };

            BrandCollection.Add(brand);
        }

        private void PlusCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            CountryDataGrid.Visibility = Visibility.Visible;
            AddCountryBtn.Visibility = Visibility.Visible;
            CountryEditBtn.Visibility = Visibility.Visible;
        }

        private void MinusCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            CountryDataGrid.Visibility = Visibility.Collapsed;
            AddCountryBtn.Visibility = Visibility.Collapsed;
            CountryEditBtn.Visibility = Visibility.Collapsed;
        }

        private void CountryDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Country country = (Country)CountryDataGrid.SelectedItem;

            if (country != null)
            {
                country.Changed = true;
            }
        }

        private void CountryDeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            Country country = (Country)CountryDataGrid.SelectedItem;

            string messageBoxText = $"Are you sure you want to delete county with id:  {country.Id}";
            string caption = "Delete dialog box";
            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            if (result == MessageBoxResult.Yes)
            {
                bool queryResult = admOp.deleteCountyFromDataBase(country);

                if (queryResult)
                {
                    CountryCollection.Remove(country);

                    CarCollection = admOp.initializeCarData();
                    CarDataGrid.ItemsSource = CarCollection;

                    BrandCollection = admOp.initializeBrandData();
                    BrandDataGrid.ItemsSource = BrandCollection;
                }
                else
                {
                    MessageBox.Show("Failed to delete country info");
                }
            }
        }

        private void CountryEditBtn_Click(object sender, RoutedEventArgs e)
        {
            bool result = true;

            foreach (Country country in CountryDataGrid.Items)
            {
                if (country.Changed)
                {
                    bool queryResult = admOp.saveCountryChanges(country);

                    if (!queryResult)
                    {
                        MessageBox.Show($"Failed to save country {country.Id}");
                        result = false;
                    }
                }
            }

            if (result)
            {
                CountryCollection = admOp.initializeCountryData();
                CountryDataGrid.ItemsSource = CountryCollection;

                MessageBox.Show("Successfully updated country info");

                NavigationService.Refresh();
            }
        }

        private void AddCountryBtn_Click(object sender, RoutedEventArgs e)
        {
            Country country = new Country()
            {
                Changed = true
            };

            CountryCollection.Add(country);
        }

        private void fileBtn_Click(object sender, RoutedEventArgs e)
        {
            Car car = (Car)CarDataGrid.SelectedItem;

            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Choose an image";
                dlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";

                bool? result = dlg.ShowDialog();
                if (result ?? false)
                {
                    if (result == true)
                    {
                        car.Image = new BitmapImage(new Uri(dlg.FileName));

                        car.Changed = true;

                        (((sender as Button).Parent as StackPanel).Parent as Popup).IsOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }

}
