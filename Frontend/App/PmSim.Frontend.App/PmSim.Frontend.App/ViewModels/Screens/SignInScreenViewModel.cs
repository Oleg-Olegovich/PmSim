using PmSim.Frontend.App.ViewModels.Windows;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SignInScreenViewModel : BasicScreenViewModel
{
    public SignInScreenViewModel(BasicWindowViewModel baseWindow, 
        BasicScreenViewModel? previousScreen = null) 
        : base(baseWindow, previousScreen)
    {
    }
}