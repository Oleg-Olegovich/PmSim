using PmSim.Frontend.App.ViewModels.Windows;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class ErrorScreenViewModel : BasicScreenViewModel
{
    public string ErrorMessage { get; }

    public ErrorScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel? previous, string errorMessage)
        : base(baseWindow, previous)
        => ErrorMessage = errorMessage;
}