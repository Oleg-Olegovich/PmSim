using JetBrains.Annotations;
using PmSim.Frontend.App.ViewModels.Windows;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class EmailConfirmationScreenViewModel : BasicScreenViewModel
{
    public EmailConfirmationScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous) 
        : base(baseWindow, previous)
    {
    }
}