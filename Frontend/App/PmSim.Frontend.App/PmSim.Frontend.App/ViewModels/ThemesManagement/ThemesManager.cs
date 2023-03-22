using System;
using System.Linq;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace PmSim.Frontend.App.ViewModels.ThemesManagement;

/// <summary>
/// Changing the style files location will result in RE.
/// </summary>
public class ThemesManager
{
    /// <summary>
    /// The namespace of style definitions.
    /// </summary>
    private static readonly string _folder
        = "avares://PmSim.Frontend.App/Styles/";

    /// <summary>
    /// The name of the file with the theme definition.
    /// </summary>
    private static readonly string _themeFileName
        = "Theme/ThemeStyle.axaml";

    private static readonly Uri _stylesBaseUri = new("clr-namespace:PmSim.Frontend.App");

    /// <summary>
    /// The array of all themes.
    /// </summary>
    private static readonly StyleInclude[] _themes
        = new StyleInclude[Enum.GetNames(typeof(Themes)).Length];

    private static ThemesManager _instance = new();

    private Themes _theme;

    /// <summary>
    /// Sets the required theme style for the application.
    /// </summary>
    public static Themes Theme
    {
        set
        {
            _instance._theme = value;
            var app = Application.Current;
            if (app == null)
            {
                return;
            }
            
            app.Styles[0] = _themes[(int)value];
            // Avalonia developers are to blame for this crutch.
            app.RequestedThemeVariant = value.ToString().StartsWith("Dark") 
                ? ThemeVariant.Dark 
                : ThemeVariant.Light;
        }
    }

    public static string[] ThemeNames { get; }
        = Enum.GetValues(typeof(Themes)).Cast<Themes>().Select(x => x.ToString()).ToArray();

    static ThemesManager()
        => InitializeThemes();

    /// <summary>
    /// Initializes a style by url.
    /// </summary>
    private static StyleInclude InitializeStyle(string url)
    {
        return new StyleInclude(_stylesBaseUri)
        {
            Source = new Uri(url)
        };
    }

    /// <summary>
    /// Initialize all themes;
    /// </summary>
    private static void InitializeThemes()
    {
        for (var i = 0; i < _themes.Length; ++i)
        {
            var fullThemeFileName = _folder + ThemeNames[i] + _themeFileName;
            _themes[i] = InitializeStyle(fullThemeFileName);
        }
    }
}