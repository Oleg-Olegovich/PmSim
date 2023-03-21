using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SignInScreenViewModel : BasicScreenViewModel
{
    private TitleScreenViewModel _titleScreen;
    
    private string _login = "";
    
    public string Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
        
    private string _password = "";
    
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    private bool _isDataRemembered = true;
    
    public bool IsDataRemembered
    {
        get => _isDataRemembered;
        set => this.RaiseAndSetIfChanged(ref _isDataRemembered, value);
    }
    
    public ReactiveCommand<Unit, Unit> SignUpCommand { get; }
    
    public ReactiveCommand<Unit, Unit> GamesListCommand { get; }
    
    public SignInScreenViewModel(BasicWindowViewModel baseWindow, TitleScreenViewModel previous) 
        : base(baseWindow, previous)
    {
        _titleScreen = previous;
        SignUpCommand = ReactiveCommand.Create(ProcessSignUpClick);
        GamesListCommand = ReactiveCommand.Create(OpenGamesList);
    }

    private void OpenGamesList()
    {
        var client = new PmSimClient();
        var gamesListScreen = new GamesListScreenViewModel(BaseWindow, _titleScreen, client);
        BaseWindow.Content = gamesListScreen;
    }

    private void ProcessSignUpClick()
        => BaseWindow.Content = new SignUpScreenViewModel(BaseWindow, this);
}