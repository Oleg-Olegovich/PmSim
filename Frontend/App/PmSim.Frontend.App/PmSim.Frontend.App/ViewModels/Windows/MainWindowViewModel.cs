using PmSim.Frontend.App.ViewModels.Screens;
using Serilog.Core;

namespace PmSim.Frontend.App.ViewModels.Windows;

/// <summary>
/// Implements the logic of the main application window.
/// </summary>
public class MainWindowViewModel : BasicWindowViewModel
{
    public MainWindowViewModel(AppOptions options, Logger logger) 
        : base(options, logger)
    {
        Content = new TitleScreenViewModel(this);
    }
}