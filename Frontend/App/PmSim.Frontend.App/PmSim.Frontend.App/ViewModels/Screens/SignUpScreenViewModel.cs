using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SignUpScreenViewModel : BasicScreenViewModel
{
    private string _email = "";
    
    public string Email
    {
        get => _email;
        set => this.RaiseAndSetIfChanged(ref _email, value);
    }
    
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
    
    private string _repeatedPassword = "";
    
    public string RepeatedPassword
    {
        get => _repeatedPassword;
        set => this.RaiseAndSetIfChanged(ref _repeatedPassword, value);
    }
    
    public SignUpScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous) 
        : base(baseWindow, previous, previous)
    {
    }
}