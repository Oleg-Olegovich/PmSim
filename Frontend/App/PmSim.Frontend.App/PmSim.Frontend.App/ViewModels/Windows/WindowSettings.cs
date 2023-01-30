using Avalonia.Controls;
using PmSim.Frontend.App.ViewModels.LanguagesManager;
using PmSim.Frontend.App.ViewModels.ThemesManagement;

namespace PmSim.Frontend.App.ViewModels.Windows;

/// <summary>
/// Application window settings.
/// </summary>
public class WindowSettings
{
    /// <summary>
    /// The default settings are loaded from the application configuration.
    /// </summary>
    public static WindowSettings? Default { get; set; }

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
}