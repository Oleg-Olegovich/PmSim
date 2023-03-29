using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.ThemesManagement;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client.LanguagesManager;
using PmSim.Shared.Contracts.Enums;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

/// <summary>
/// Implements the logic of the application settings screen.
/// </summary>
public class AppOptionsScreenViewModel : BasicScreenViewModel
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
            BaseWindow.Options.Language = language;
            this.RaiseAndSetIfChanged(ref _currentLanguageIndex, value);
            BaseWindow.Content = new AppOptionsScreenViewModel(BaseWindow, _previous);
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
            BaseWindow.Options.Theme = theme;
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

    public AppOptionsScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous)
        : base(baseWindow, previous)
    {
        _previous = previous;
        _currentLanguageIndex = (int)baseWindow.Options.Language;
        _currentThemeIndex = (int)baseWindow.Options.Theme;
        _isFullscreen = baseWindow.IsFullscreen;
    }
}