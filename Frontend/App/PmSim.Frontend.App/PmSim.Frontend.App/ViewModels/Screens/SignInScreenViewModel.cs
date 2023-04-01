using System.Reactive;
using System.Threading.Tasks;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Exceptions;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SignInScreenViewModel : BasicScreenViewModel
{
    private readonly TitleScreenViewModel _titleScreen;
    
    private string _login = "";
    
    public string Login
    {
        get => _login;
        set
        {
            this.RaiseAndSetIfChanged(ref _login, value);
            CheckIsSignInAvailable();
            if (IsDataRemembered)
            {
                BaseWindow.Options.AutofillUserData.Login = value;
            }
        }
    }

    private string _password = "";
    
    public string Password
    {
        get => _password;
        set
        {
            this.RaiseAndSetIfChanged(ref _password, value);
            CheckIsSignInAvailable();
            if (IsDataRemembered)
            {
                BaseWindow.Options.AutofillUserData.Password = value;
            }
        }
    }

    private bool _isDataRemembered;
    
    public bool IsDataRemembered
    {
        get => _isDataRemembered;
        set
        {
            this.RaiseAndSetIfChanged(ref _isDataRemembered, value);
            BaseWindow.Options.AutofillUserData.IsMultiplayerDataRemembered = value;
            if (!value)
            {
                BaseWindow.Options.AutofillUserData.Login = BaseWindow.Options.AutofillUserData.Password = null;
                return;
            }
            BaseWindow.Options.AutofillUserData.Login = Login;
            BaseWindow.Options.AutofillUserData.Password = Password;
        }
    }

    private bool _isSignInAvailable;

    public bool IsSignInAvailable
    {
        get => _isSignInAvailable;
        set => this.RaiseAndSetIfChanged(ref _isSignInAvailable, value);
    }

    public ReactiveCommand<Unit, Unit> SignUpCommand { get; }
    
    public ReactiveCommand<Unit, Unit> SignInCommand { get; }
    
    public SignInScreenViewModel(BasicWindowViewModel baseWindow, TitleScreenViewModel titleScreen) 
        : base(baseWindow, titleScreen)
    {
        _titleScreen = titleScreen;
        SignUpCommand = ReactiveCommand.Create(OpenSignUpScreen);
        SignInCommand = ReactiveCommand.Create(SignIn);
        _isDataRemembered = baseWindow.Options.AutofillUserData.IsMultiplayerDataRemembered;
        if (!_isDataRemembered)
        {
            return;
        }
        Login = baseWindow.Options.AutofillUserData.Login ?? "";
        Password = baseWindow.Options.AutofillUserData.Password ?? "";
    }

    private void CheckIsSignInAvailable()
        => IsSignInAvailable = Login != "" && Password != "";
    
    private void SignIn()
    {
        try
        {
            var client = MultiplayerClient.SignInAsync(Login, Password);
            var gamesListScreen = new GamesListScreenViewModel(BaseWindow, _titleScreen, client);
            BaseWindow.Content = gamesListScreen;
        }
        catch (PmSimException exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
    }

    private void OpenSignUpScreen()
        => BaseWindow.Content = new SignUpScreenViewModel(BaseWindow, this, _titleScreen);
}