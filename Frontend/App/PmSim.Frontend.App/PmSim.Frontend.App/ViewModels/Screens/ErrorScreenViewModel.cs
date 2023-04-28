using PmSim.Frontend.App.ViewModels.Windows;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class ErrorScreenViewModel : BasicScreenViewModel
{
    public string ErrorMessage { get; }

    public ErrorScreenViewModel(MainViewModel mainView, BasicScreenViewModel? previous, string errorMessage)
        : base(mainView, previous)
        => ErrorMessage = errorMessage;
}