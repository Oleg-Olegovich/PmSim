using Avalonia.Controls;
using PmSim.Frontend.App.Models;
using PmSim.Frontend.App.ViewModels.ThemesManagement;
using PmSim.Frontend.Client.LanguagesManager;
using PmSim.Shared.Contracts.Enums;

namespace PmSim.Frontend.App.ViewModels.Windows;

/// <summary>
/// Application window settings.
/// </summary>
public class AppOptions
{
    /// <summary>
    /// The default settings are loaded from the application configuration.
    /// </summary>
    public static AppOptions? Default { get; set; }

    /// <summary>
    /// Current language.
    /// </summary>
    public Languages Language { get; set; }

    /// <summary>
    /// Current color theme.
    /// </summary>
    public Themes Theme { get; set; }

    /// <summary>
    /// Current window state (fullscreen, windowed, etc.).
    /// </summary>
    public WindowState WindowState { get; set; }

    /// <summary>
    /// Size of the window, if WindowState is Normal.
    /// </summary>
    public Size Size { get; set; } = new();

    /// <summary>
    /// Forms autofill data.
    /// </summary>
    public AutofillUserData AutofillUserData { get; set; } = new();
}