using PmSim.Frontend.App.ViewModels.Windows;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SingleSignInScreenViewModel : BasicScreenViewModel
{
    public SingleSignInScreenViewModel(BasicWindowViewModel baseWindow, 
        BasicScreenViewModel? previousScreen = null) 
        : base(baseWindow, previousScreen)
    {
    }
}