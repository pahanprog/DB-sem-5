using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfApp1.Models;
using WpfApp1.Operations;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = tbxUsername.Text;
            string password = pbxPassword.Password;

            UserOperatioms uop = new UserOperatioms();

            User user = uop.LoginUser(username, password);

            if (user == null)
            {
                return;
            }

            Globals.LoggedInUser = user;

            NavigationService.Navigate(new MainMenu());
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
