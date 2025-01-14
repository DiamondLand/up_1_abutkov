using System;
using System.Windows.Input;
using WpfApp1;
using WpfApp1.Helpers;
using WpfApp1.Services;
using WpfApp1.ViewModels;

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

    public event Action OnLoginSuccess;

    private void OnLogin()
    {
        var user = _authenticationService.Login(Username, Password);

        if (user != null)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Неверные данные для входа.");
        }
    }
}
