using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SingleSignInScreenViewModel : BasicScreenViewModel
{
    private string _login = "";
    
    public string Login
    {
        get => _login;
        set => this.RaiseAndSetIfChanged(ref _login, value);
    }
        
    private bool _isDataRemembered = true;
    
    public bool IsDataRemembered
    {
        get => _isDataRemembered;
        set => this.RaiseAndSetIfChanged(ref _isDataRemembered, value);
    }
    
    public SingleSignInScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous) 
        : base(baseWindow, previous, new GameOptionsScreenViewModel(baseWindow, previous, new PmSimClient()))
    { }
}