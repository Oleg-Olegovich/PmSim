using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Api;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SingleSignInScreenViewModel : BasicScreenViewModel
{
    private string _login = "";
    
    public string Login
    {
        get => _login;
        set
        {
            this.RaiseAndSetIfChanged(ref _login, value);
            if (IsDataRemembered && MainView.MainWindow != null)
            {
                MainView.Options.AutofillUserData.SingleLogin = Login;
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
            
            MainView.Options.AutofillUserData.IsSingleDataRemembered = value;
            if (value)
            {
                MainView.Options.AutofillUserData.SingleLogin = Login;
                return;
            }

            MainView.Options.AutofillUserData.SingleLogin = null;
        }
    }

    public ReactiveCommand<Unit, Unit> NextCommand { get; }

    public SingleSignInScreenViewModel(MainViewModel mainView, BasicScreenViewModel previous)
        : base(mainView, previous)
    {
        NextCommand = ReactiveCommand.Create(OpenGameOptionsScreen);
        if (mainView.MainWindow is null)
        {
            return;
        }
        
        _isDataRemembered = MainView.Options.AutofillUserData.IsSingleDataRemembered;
        if (_isDataRemembered)
        {
            Login = MainView.Options.AutofillUserData.SingleLogin ?? "";
        }
    }

    private void OpenGameOptionsScreen()
    {
        var client = new SinglePlayerClient(Login);
        MainView.Content = new GameOptionsScreenViewModel(MainView, this, client, true);
    }
}