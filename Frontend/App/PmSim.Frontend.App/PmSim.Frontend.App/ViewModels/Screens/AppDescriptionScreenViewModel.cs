using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Windows;

namespace PmSim.Frontend.App.ViewModels.Screens;

/// <summary>
/// Contains the data of the application description screen.
/// </summary>
public class AppDescriptionScreenViewModel : BasicScreenViewModel
{
    public override string Header => LocalizationAppDescriptionScreen.TextBlockHeader;

    public AppDescriptionScreenViewModel(BasicWindowViewModel baseWindow) 
        : base(baseWindow)
    { }
}