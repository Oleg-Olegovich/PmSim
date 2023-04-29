using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
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
            if (IsDataRemembered && MainView.MainWindow != null)
            {
                MainView.Options.AutofillUserData.Login = value;
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
            if (IsDataRemembered && MainView.MainWindow != null)
            {
                MainView.Options.AutofillUserData.Password = value;
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
            if (MainView.MainWindow is null)
            {
                return;
            }
            
            MainView.Options.AutofillUserData.IsMultiplayerDataRemembered = value;
            if (!value)
            {
                MainView.Options.AutofillUserData.Login 
                    = MainView.Options.AutofillUserData.Password = null;
                return;
            }

            MainView.Options.AutofillUserData.Login = Login;
            MainView.Options.AutofillUserData.Password = Password;
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

    public SignInScreenViewModel(MainViewModel mainView, TitleScreenViewModel titleScreen)
        : base(mainView, titleScreen)
    {
        _titleScreen = titleScreen;
        SignUpCommand = ReactiveCommand.Create(OpenSignUpScreen);
        SignInCommand = ReactiveCommand.Create(SignIn);
        if (mainView.MainWindow is null)
        {
            return;
        }
        
        _isDataRemembered = MainView.Options.AutofillUserData.IsMultiplayerDataRemembered;
        if (!_isDataRemembered)
        {
            return;
        }

        Login = MainView.Options.AutofillUserData.Login ?? "";
        Password = MainView.Options.AutofillUserData.Password ?? "";
    }

    private void CheckIsSignInAvailable()
        => IsSignInAvailable = Login != "" && Password != "";

    private void SignIn()
    {
        try
        {
            var client = MultiPlayerClient.SignIn(Login, Password);
            if (client.IsSubscriptionPaid)
            {
            }

            var gamesListScreen = new GamesListScreenViewModel(MainView, _titleScreen, client);
            MainView.Content = client.IsSubscriptionPaid
                ? gamesListScreen
                : new SubscriptionPurchaseScreenViewModel(MainView, this, gamesListScreen, true);
        }
        catch (PmSimException exception)
        {
            MainView.Content = new ErrorScreenViewModel(MainView, this, exception.Message);
        }
    }

    private void OpenSignUpScreen()
        => MainView.Content = new SignUpScreenViewModel(MainView, this, _titleScreen);
}