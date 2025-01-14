using System.Windows.Input;
using WpfApp1.Helpers;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        private string _password;

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ICommand LoginCommand { get; }

        private readonly AuthenticationService _authenticationService;

        public LoginViewModel(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            LoginCommand = new RelayCommand(OnLogin);
        }

        private void OnLogin()
        {
            var user = _authenticationService.Login(Username, Password);

            if (user != null)
            {
                // Успешный вход
                System.Diagnostics.Debug.WriteLine($"Вход успешен для пользователя: {user.Username}");
                // Здесь можно переходить на другую страницу или делать что-то еще
            }
            else
            {
                // Неверные данные
                System.Diagnostics.Debug.WriteLine("Неверные данные для входа.");
            }
        }
    }
}
