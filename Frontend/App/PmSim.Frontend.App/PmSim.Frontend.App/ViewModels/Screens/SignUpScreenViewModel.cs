using System.Reactive;
using System.Threading.Tasks;
using PmSim.Frontend.App.Models;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Api;
using PmSim.Frontend.Client.Dto;
using PmSim.Frontend.Client.Utils;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.User;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

// Нужно добавить TextBlock для ошибки.
public class SignUpScreenViewModel : BasicScreenViewModel
{
    private readonly TitleScreenViewModel _titleScreen;
    
    private string _email = "";
    
    public string Email
    {
        get => _email;
        set
        {
            this.RaiseAndSetIfChanged(ref _email, value);
            CheckIsSignUpAvailable();
        }
    }

    private string _login = "";
    
    public string Login
    {
        get => _login;
        set
        {
            this.RaiseAndSetIfChanged(ref _login, value);
            CheckIsSignUpAvailable();
        }
    }

    private string _password = "";
    
    public string Password
    {
        get => _password;
        set
        {
            this.RaiseAndSetIfChanged(ref _password, value);
            CheckIsSignUpAvailable();
        }
    }

    private string _repeatedPassword = "";
    
    public string RepeatedPassword
    {
        get => _repeatedPassword;
        set
        {
            this.RaiseAndSetIfChanged(ref _repeatedPassword, value);
            CheckIsSignUpAvailable();
        }
    }

    private string _errorMessage = "";
    
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    
    private bool _isSignUpAvailable;
    
    public bool IsSignUpAvailable
    {
        get => _isSignUpAvailable;
        set => this.RaiseAndSetIfChanged(ref _isSignUpAvailable, value);
    }
    
    public ReactiveCommand<Unit, Unit> SignUpCommand { get; }
    
    public SignUpScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous,
        TitleScreenViewModel titleScreen) 
        : base(baseWindow, previous)
    {
        _titleScreen = titleScreen;
        SignUpCommand = ReactiveCommand.Create(SignUp);
    }

    private void CheckIsSignUpAvailable()
    {
        if (!Validator.IsEmailCorrect(Email))
        {
            ErrorMessage = LocalizationSignUpScreen.IncorrectEmail;
            IsSignUpAvailable = false;
            return;
        }
        if (!Validator.IsLoginCorrect(Login))
        {
            ErrorMessage = LocalizationSignUpScreen.IncorrectLogin;
            IsSignUpAvailable = false;
            return;
        }
        if (!Validator.IsPasswordCorrect(Password))
        {
            ErrorMessage = LocalizationSignUpScreen.IncorrectPassword;
            IsSignUpAvailable = false;
            return;
        }
        if (Password != RepeatedPassword)
        {
            ErrorMessage = LocalizationSignUpScreen.IncorrectRepeatedPassword;
            IsSignUpAvailable = false;
            return;
        }

        ErrorMessage = "";
        IsSignUpAvailable = true;
    }

    private void SignUp()
    {
        try
        {
            if (!MultiPlayerClient.ReserveLogin(Login))
            {
                ErrorMessage = LocalizationSignUpScreen.LoginIsOccupied;
                return;
            }

            ErrorMessage = "";
            var code = MultiPlayerClient.SendCodeToEmailAsync(Email);
            var user = new User(Email, Login, Password);
            BaseWindow.Content = new EmailConfirmationScreenViewModel(
                BaseWindow, this, _titleScreen, user, code);
        }
        catch (PmSimException exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
    }
}