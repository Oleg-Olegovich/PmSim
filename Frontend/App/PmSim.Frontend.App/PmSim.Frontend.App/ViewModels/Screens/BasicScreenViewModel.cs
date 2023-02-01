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
    private readonly BasicScreenViewModel? _previousScreen, _nextScreen;

    // <summary>
    /// Returns to the previous screen.
    /// If previous screen is null, returns to the title screen.
    /// </summary>
    public ReactiveCommand<Unit, Unit> BackCommand { get; }
    
    public ReactiveCommand<Unit, Unit> NextCommand { get; }

    /// <summary>
    /// Reference to the base window of the 
    /// application where the controls are located.
    /// </summary>
    public BasicWindowViewModel BaseWindow { get; }

    protected BasicScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel? previous = null, 
        BasicScreenViewModel? nextScreen = null)
    {
        BaseWindow = baseWindow;
        _previousScreen = previous;
        _nextScreen = nextScreen;
        BackCommand = ReactiveCommand.Create(ProcessBackClick);
        NextCommand = ReactiveCommand.Create(ProcessNextClick);
    }
    
    private void ProcessNextClick()
        => BaseWindow.Content = _nextScreen;

    private void ProcessBackClick()
        => BaseWindow.Content = _previousScreen ?? new TitleScreenViewModel(BaseWindow);
}