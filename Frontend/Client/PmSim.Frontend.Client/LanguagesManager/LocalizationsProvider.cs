using System.Globalization;

namespace PmSim.Frontend.Client.LanguagesManager;

/// <summary>
/// Changing the namespace of this class will result in RE.
/// </summary>
public class LocalizationsProvider
{
    /// <summary>
    /// Names of language localizations, each in its own language.
    /// </summary>
    public static string[] LocalizationNames { get; }

    /// <summary>
    /// Sets localization.
    /// </summary>
    public static Languages Localization
    {
        set
        {
            Thread.CurrentThread.CurrentUICulture = value switch
            {
                Languages.English => CultureInfo.GetCultureInfo("en-US"),
                Languages.Russian => CultureInfo.GetCultureInfo("ru-RU"),
                _ => CultureInfo.GetCultureInfo("ru-RU")
            };
        }
    }

    static LocalizationsProvider()
    {
        var localizationNumber = Enum.GetNames(typeof(Languages)).Length;
        LocalizationNames = new string[localizationNumber];
        for (var i = 0; i < localizationNumber; ++i)
        {
            LocalizationNames[i] = GetLocalizationName((Languages)i);
        } 
    }

    private static string GetLocalizationName(Languages language)
        => language switch
        {
            Languages.Russian => "Русский",
            Languages.English => "English",
            _ => "Unknown language"
        };
}