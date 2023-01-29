using System.Reactive;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

/// <summary>
/// The title screen of the application.
/// </summary>
public class TitleScreenViewModel : BasicScreenViewModel
{
    public override string Header => LocalizationTitleScreen.TextBlockHeader;

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

    private void OpenSettingsScreen()
        => BaseWindow.Content = new AppSettingsScreenViewModel(BaseWindow);

    private void OpenAppDescriptionScreen()
        => BaseWindow.Content = new AppDescriptionScreenViewModel(BaseWindow);
}