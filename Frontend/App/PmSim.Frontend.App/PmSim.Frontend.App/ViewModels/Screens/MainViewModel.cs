using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class MainViewModel : ViewModelBase
{
    /// <summary>
    /// Reference to the changing content of the window.
    /// </summary>
    private BasicScreenViewModel? _content;

    /// <summary>
    /// Implements getting access to the reference to 
    /// the changing content of the window.
    /// </summary>
    public BasicScreenViewModel? Content
    {
        get => _content;
        set => this.RaiseAndSetIfChanged(ref _content, value);
    }

    private readonly AppOptions _options = new AppOptions();
    
    public AppOptions Options => MainWindow is null ? _options : MainWindow.Options;

    public MainWindowViewModel? MainWindow { get; }

    public MainViewModel(MainWindowViewModel? mainWindow = null)
    {
        MainWindow = mainWindow;
        Content = new TitleScreenViewModel(this);
    }
}