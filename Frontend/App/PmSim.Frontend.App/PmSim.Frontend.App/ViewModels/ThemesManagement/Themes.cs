namespace PmSim.Frontend.App.ViewModels.ThemesManagement;

/// <summary>
/// List of names of color themes.
/// To add a new theme, you need to add to this list and create a subdirectory
/// in the Styles directory with the same name and a postfix Theme.
/// </summary>
public enum Themes
{
    LightSimple,
    LightFluent,
    DarkSimple,
    DarkFluent
}