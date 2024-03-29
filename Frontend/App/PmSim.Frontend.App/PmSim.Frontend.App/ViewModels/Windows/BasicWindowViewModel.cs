using System;
using Avalonia.Controls;
using PmSim.Frontend.App.ViewModels.ThemesManagement;
using PmSim.Frontend.Client.FileManagement;
using PmSim.Frontend.Client.LanguagesManager;
using ReactiveUI;
using Serilog.Core;

namespace PmSim.Frontend.App.ViewModels.Windows;

/// <summary>
/// Base class of application windows.
/// </summary>
public abstract class BasicWindowViewModel : ViewModelBase
{
    /// <summary>
    /// Handler for closing the application.
    /// </summary>
    public EventHandler<WindowClosingEventArgs> ClosingHandler { get; }

    public AppOptions Options { get; }

    private double _width;

    /// <summary>
    /// The current width of the window binding to the view.
    /// </summary>
    public double Width
    {
        get => _width;
        set => this.RaiseAndSetIfChanged(ref _width, value);
    }

    private double _height;

    /// <summary>
    /// The current height of the window binding to the view.
    /// </summary>
    public double Height
    {
        get => _height;
        set => this.RaiseAndSetIfChanged(ref _height, value);
    }

    private WindowState _windowState, _previousWindowState;

    /// <summary>
    /// The current state of the window binding to the view.
    /// </summary>
    public WindowState WindowState
    {
        get => _windowState;
        set
        {
            if (_windowState != WindowState.FullScreen)
            {
                _previousWindowState = _windowState;
            }

            this.RaiseAndSetIfChanged(ref _windowState, value);
        }
    }

    /// <summary>
    /// It is necessary to return to the previous state of
    /// the window when the fullscreen mode is turned off.
    /// </summary>
    public bool IsFullscreen
    {
        get => WindowState == WindowState.FullScreen;
        set
        {
            if (value)
            {
                Options.WindowState = WindowState = WindowState.FullScreen;
            }
            else
            {
                Options.WindowState = WindowState = _previousWindowState;
                Options.Size = Size.Default ?? throw new NullReferenceException("Default size is null!");
                Width = Options.Size.Width;
                Height = Options.Size.Height;
            }
        }
    }

    /// <summary>
    /// It is recommended to use this object for all logging.
    /// Writes to a file in the program data directory.
    /// </summary>
    public Logger Logger { get; }

    protected BasicWindowViewModel(AppOptions options, Logger logger)
    {
        Options = options;
        Logger = logger;
        Logger.Information("A window has been created");
        ClosingHandler = ProcessClosing;
        InitializeWindowSettings();
    }

    private void InitializeWindowSettings()
    {
        LocalizationsProvider.Localization = Options.Language;
        ThemesManager.Theme = Options.Theme;
        Width = Options.Size.Width;
        Height = Options.Size.Height;
        WindowState = Options.WindowState;
    }

    private void ProcessClosing(object? sender, WindowClosingEventArgs args)
    {
        try
        {
            if (sender is not Window window)
            {
                throw new ArgumentException("Closing event sender is not window!");
            }

            Options.Size.Width = window.Width;
            Options.Size.Height = window.Height;
            Options.WindowState = window.WindowState;
            FileManager.SaveConfiguration(Options);
            Logger.Information("The window is successfully closed");
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Error when closing the window");
        }
    }
}