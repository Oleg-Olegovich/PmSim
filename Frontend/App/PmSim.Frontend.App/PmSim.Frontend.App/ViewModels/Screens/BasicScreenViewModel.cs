using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

/// <summary>
/// The base class of the application screen.
/// The screen is the content of the window.
/// </summary>
public abstract class BasicScreenViewModel : ViewModelBase
{
    private BasicScreenViewModel? _previousScreen;
    
    /// <summary>
    /// The title is displayed in the status bar.
    /// </summary>
    public abstract string Header { get; }
    
    // <summary>
    /// Returns to the previous screen.
    /// </summary>
    public ReactiveCommand<Unit, Unit> BackCommand { get; }

    /// <summary>
    /// Reference to the base window of the 
    /// application where the controls are located.
    /// </summary>
    public BasicWindowViewModel BaseWindow { get; }

    protected BasicScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel? previousScreen = null)
    {
        BaseWindow = baseWindow;
        BackCommand = ReactiveCommand.Create(ProcessBackClick);
        _previousScreen = previousScreen;
    }

    protected virtual void ProcessBackClick()
        => BaseWindow.Content = _previousScreen;
}