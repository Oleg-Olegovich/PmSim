using System.Reactive;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.User;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class EmailConfirmationScreenViewModel : BasicScreenViewModel
{
    private readonly TitleScreenViewModel _titleScreen;
    
    private readonly string _code;
    
    private readonly User _user;
    
    private string _inputCode = "";
    
    public string InputCode
    {
        get => _inputCode;
        set
        {
            this.RaiseAndSetIfChanged(ref _inputCode, value);
            IsConfirmationAvailable = value != "";
        }
    }

    private bool _isConfirmationAvailable;
    
    public bool IsConfirmationAvailable
    {
        get => _isConfirmationAvailable;
        set => this.RaiseAndSetIfChanged(ref _isConfirmationAvailable, value);
    }
    
    private string _errorMessage = "";
    
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    
    public ReactiveCommand<Unit, Unit> ConfirmCommand { get; }
    
    public EmailConfirmationScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous,
        TitleScreenViewModel titleScreen, User user, string code) : base(baseWindow, previous)
    {
        BaseWindow.Logger.Information("Send code to Email: {Code}", code);
        _user = user;
        _titleScreen = titleScreen;
        _code = code;
        ConfirmCommand = ReactiveCommand.Create(ProcessEmailConfirmation);
    }

    private void ProcessEmailConfirmation()
    {
        if (_code != InputCode)
        {
            ErrorMessage = LocalizationEmailConfirmationScreen.WrongCode;
            return;
        }

        try
        {
            var client = MultiPlayerClient.SignUp(_user);
            var gamesListScreen = new GamesListScreenViewModel(BaseWindow, _titleScreen, client);
            var subscriptionPurchaseScreen 
                = new SubscriptionPurchaseScreenViewModel(BaseWindow, _titleScreen, gamesListScreen, true);
            BaseWindow.Content = subscriptionPurchaseScreen;
        }
        catch (PmSimException exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
    }
}