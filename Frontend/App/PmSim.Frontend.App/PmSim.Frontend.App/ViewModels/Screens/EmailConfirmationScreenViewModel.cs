using System.Reactive;
using System.Threading.Tasks;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Api;
using PmSim.Frontend.Client.Dto;
using PmSim.Frontend.Client.Exceptions;
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
        _user = user;
        _titleScreen = titleScreen;
        _code = code;
        ConfirmCommand = ReactiveCommand.CreateFromTask(ProcessEmailConfirmation);
    }

    private async Task ProcessEmailConfirmation()
    {
        if (_code != InputCode)
        {
            ErrorMessage = LocalizationEmailConfirmationScreen.WrongCode;
            return;
        }

        try
        {
            var client = await MultiplayerClient.SignUpAsync(_user);
            var gamesListScreen = new GamesListScreenViewModel(BaseWindow, _titleScreen, client);
            BaseWindow.Content = gamesListScreen;
        }
        catch (PmSimClientException exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
    }
}