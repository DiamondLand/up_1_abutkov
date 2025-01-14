using System.Windows;
using System.Windows.Controls;
using WpfApp1.Services;

namespace WpfApp1.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel(new AuthenticationService());
        }

        // Добавьте обработчик события PasswordChanged
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Получаем пароль из PasswordBox
            var passwordBox = sender as PasswordBox;
            var viewModel = DataContext as LoginViewModel;

            if (viewModel != null && passwordBox != null)
            {
                // Передаем пароль в ViewModel
                viewModel.Password = passwordBox.Password;
            }
        }

        public void OnLoginSuccess()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
