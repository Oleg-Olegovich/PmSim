using PmSim.Frontend.App.ViewModels.ThemesManagement;
using PmSim.Frontend.Client.LanguagesManager;
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
            MainView.Options.Language = language;
            LocalizationsProvider.Localization = language;
            this.RaiseAndSetIfChanged(ref _currentLanguageIndex, value);
            MainView.Content = new AppOptionsScreenViewModel(MainView, _previous);
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
            MainView.Options.Theme = theme;
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
            if (MainView.MainWindow != null)
            {
                MainView.MainWindow.IsFullscreen = value;
            }
            
            this.RaiseAndSetIfChanged(ref _isFullscreen, value);
        }
    }
    
    public bool IsWindowMode => MainView.MainWindow != null;

    public AppOptionsScreenViewModel(MainViewModel mainView, BasicScreenViewModel previous)
        : base(mainView, previous)
    {
        _previous = previous;
        if (mainView.MainWindow is null)
        {
            return;
        }
        
        _currentLanguageIndex = (int)MainView.Options.Language;
        _currentThemeIndex = (int)MainView.Options.Theme;
        _isFullscreen = mainView.MainWindow.IsFullscreen;
    }
}