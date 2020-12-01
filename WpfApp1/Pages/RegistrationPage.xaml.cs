using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

using WpfApp1.Models;
using WpfApp1.Operations;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        string appPassword = null;

        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = pbxPassword.Password;
            int status = userTypeCombo.SelectedIndex;
            if (status == 0)
            {
                status = 2;
            }

            UserOperatioms uop = new UserOperatioms();
            User user = uop.RegisterUser(username, password, status);

            if (user == null)
            {
                MessageBox.Show("Username already exists");
                return;
            }

            Globals.LoggedInUser = user;
            NavigationService.Navigate(new MainMenu());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (userTypeCombo.SelectedIndex == 1) {
                MasterPassModel.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mbxPassword.Password == appPassword )
            {
                MasterPassModel.Visibility = Visibility.Hidden;
            }
            else
            {
                MessageBox.Show("Wrong input");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            userTypeCombo.SelectedIndex = 0;
            MasterPassModel.Visibility = Visibility.Hidden;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UserOperatioms uop = new UserOperatioms();
            appPassword = uop.GetPassword();
        }
    }
}
