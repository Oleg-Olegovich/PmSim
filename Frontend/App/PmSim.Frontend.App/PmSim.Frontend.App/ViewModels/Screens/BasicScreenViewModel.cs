using System.Reactive;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

/// <summary>
/// The base class of the application screen.
/// The screen is the content of the window.
/// </summary>
public abstract class BasicScreenViewModel : ViewModelBase
{
    private readonly BasicScreenViewModel? _previousScreen;

    // <summary>
    /// Returns to the previous screen.
    /// If previous screen is null, returns to the title screen.
    /// </summary>
    public ReactiveCommand<Unit, Unit> BackCommand { get; }
    
    public MainViewModel MainView { get; }

    protected BasicScreenViewModel(MainViewModel mainView, BasicScreenViewModel? previous = null)
    {
        MainView = mainView;
        _previousScreen = previous;
        BackCommand = ReactiveCommand.Create(ProcessBackClick);
    }

    private void ProcessBackClick()
        => MainView.Content = _previousScreen ?? new TitleScreenViewModel(MainView);
}