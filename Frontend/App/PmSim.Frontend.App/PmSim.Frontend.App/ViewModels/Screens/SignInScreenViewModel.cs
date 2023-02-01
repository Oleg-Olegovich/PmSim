using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SignInScreenViewModel : BasicScreenViewModel
{
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
    
    public SignInScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous) 
        : base(baseWindow, previous, new GamesListScreenViewModel(baseWindow, previous))
    {
        SignUpCommand = ReactiveCommand.Create(ProcessSignUpClick);
    }
    
    private void ProcessSignUpClick()
        => BaseWindow.Content = new SignUpScreenViewModel(BaseWindow, this);
}