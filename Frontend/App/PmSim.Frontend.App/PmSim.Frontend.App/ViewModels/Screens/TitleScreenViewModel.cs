using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

/// <summary>
/// The title screen of the application.
/// </summary>
public class TitleScreenViewModel : BasicScreenViewModel
{
    public ReactiveCommand<Unit, Unit> SingleplayerModeCommand { get; }
    
    public ReactiveCommand<Unit, Unit> MultiplayerModeCommand { get; }

    /// <summary>
    /// Opens the application settings screen.
    /// </summary>
    public ReactiveCommand<Unit, Unit> SettingsCommand { get; }

    /// <summary>
    /// Opens the application description screen.
    /// </summary>
    public ReactiveCommand<Unit, Unit> AppDescriptionCommand { get; }

    /// <summary>
    /// Closes the application.
    /// </summary>
    public ReactiveCommand<Unit, Unit> ExitCommand { get; }

    public TitleScreenViewModel(BasicWindowViewModel baseWindow)
        : base(baseWindow)
    {
        SingleplayerModeCommand = ReactiveCommand.Create(OpenSingleplayerGameScreen);
        MultiplayerModeCommand = ReactiveCommand.Create(OpenMultiplayerGameScreen);
        SettingsCommand = ReactiveCommand.Create(OpenSettingsScreen);
        AppDescriptionCommand = ReactiveCommand.Create(OpenAppDescriptionScreen);
        ExitCommand = ReactiveCommand.Create(ExitApp);
    }

    private static void ExitApp()
    {
        var app = Application.Current;
        if (app?.ApplicationLifetime is ClassicDesktopStyleApplicationLifetime lifetime)
        {
            lifetime.Shutdown();
        }
    }

    private void OpenSingleplayerGameScreen()
        => BaseWindow.Content = new SingleSignInScreenViewModel(BaseWindow, this);

    private void OpenMultiplayerGameScreen()
        => BaseWindow.Content = new SignInScreenViewModel(BaseWindow, this);
    
    private void OpenSettingsScreen()
        => BaseWindow.Content = new AppSettingsScreenViewModel(BaseWindow, this);

    private void OpenAppDescriptionScreen()
        => BaseWindow.Content = new AppDescriptionScreenViewModel(BaseWindow, this);
}