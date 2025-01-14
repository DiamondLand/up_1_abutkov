using System.Windows;
using WpfApp1.ViewModels;
using WpfApp1.Services;

namespace WpfApp1.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            AuthenticationService authenticationService = new AuthenticationService();
            DataContext = new LoginViewModel(authenticationService);
        }

        public void OnLoginSuccess()
        {
            // После успешного входа, открываем главное окно и закрываем окно логина
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
