using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.LanguagesManager;
using PmSim.Frontend.App.ViewModels.ThemesManagement;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

/// <summary>
/// Implements the logic of the application settings screen.
/// </summary>
public class AppSettingsScreenViewModel : BasicScreenViewModel
{
    private readonly BasicScreenViewModel _previous;
    
    /// <summary>
    /// The names of the language localizations are displayed in the view.
    /// </summary>
    public string[] LocalizationNames => LocalizationsProvider.LocalizationNames;

    /// <summary>
    /// The names of the color themes displayed in the view.
    /// </summary>
    public string[] ThemeNames => ThemesManager.ThemeNames;
    
    private int _currentLanguageIndex;

    /// <summary>
    /// Index of the currently selected language.
    /// </summary>
    public int CurrentLanguageIndex
    {
        get => _currentLanguageIndex;
        set
        {
            if (_currentLanguageIndex == value)
            {
                return;
            }

            var language = (Languages)value;
            LocalizationsProvider.Localization = language;
            BaseWindow.Settings.Language = language;
            this.RaiseAndSetIfChanged(ref _currentLanguageIndex, value);
            BaseWindow.Content = new AppSettingsScreenViewModel(BaseWindow, _previous);
        }
    }

    private int _currentThemeIndex;

    /// <summary>
    /// Index of the currently selected color theme.
    /// </summary>
    public int CurrentThemeIndex
    {
        get => _currentThemeIndex;
        set
        {
            if (_currentThemeIndex == value)
            {
                return;
            }

            var theme = (Themes)value;
            BaseWindow.Settings.Theme = theme;
            ThemesManager.Theme = theme;
            this.RaiseAndSetIfChanged(ref _currentThemeIndex, value);
        }
    }

    private bool _isFullscreen;

    /// <summary>
    /// Fullscreen mode flag, switches to view.
    /// </summary>
    public bool IsFullscreen
    {
        get => _isFullscreen;
        set
        {
            BaseWindow.IsFullscreen = value;
            this.RaiseAndSetIfChanged(ref _isFullscreen, value);
        }
    }

    private int _zoomSpeed;

    /// <summary>
    /// Adjusts the zoom speed of the image viewer on the scan screen.
    /// </summary>
    public int ZoomSpeed
    {
        get => _zoomSpeed;
        set
        {
            BaseWindow.Settings.ZoomSpeed = value;
            this.RaiseAndSetIfChanged(ref _zoomSpeed, value);
        }
    }

    public AppSettingsScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous)
        : base(baseWindow, previous)
    {
        _previous = previous;
        _currentLanguageIndex = (int)baseWindow.Settings.Language;
        _currentThemeIndex = (int)baseWindow.Settings.Theme;
        _isFullscreen = baseWindow.IsFullscreen;
        _zoomSpeed = baseWindow.Settings.ZoomSpeed;
    }
}