using System;
using System.ComponentModel;
using Avalonia.Controls;
using PmSim.Frontend.App.ViewModels.FileManagement;
using PmSim.Frontend.App.ViewModels.LanguagesManager;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Frontend.App.ViewModels.ThemesManagement;
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
    public EventHandler<CancelEventArgs> ClosingHandler { get; }

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

    private WindowSettings _settings;
    
    public WindowSettings Settings
    {
        get => _settings;
        set => this.RaiseAndSetIfChanged(ref _settings, value);
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
                Settings.WindowState = WindowState = WindowState.FullScreen;
            }
            else
            {
                Settings.WindowState = WindowState = _previousWindowState;
                Settings.Size = Size.Default ?? throw new NullReferenceException("Default size is null!");
                Width = Settings.Size.Width;
                Height = Settings.Size.Height;
            }
        }
    }

    /// <summary>
    /// It is recommended to use this object for all logging.
    /// Writes to a file in the program data directory.
    /// </summary>
    public Logger Logger { get; }

    protected BasicWindowViewModel(WindowSettings settings, Logger logger)
    {
        _settings = settings;
        Logger = logger;
        Logger.Information("A window has been created");
        ClosingHandler = ProcessClosing;
        InitializeWindowSettings();
    }

    private void InitializeWindowSettings()
    {
        LocalizationsProvider.Localization = Settings.Language;
        ThemesManager.Theme = Settings.Theme;
        Width = Settings.Size.Width;
        Height = Settings.Size.Height;
        WindowState = Settings.WindowState;
    }

    private void ProcessClosing(object? sender, CancelEventArgs args)
    {
        try
        {
            if (sender is not Window window)
            {
                throw new ArgumentException("Closing event sender is not window!");
            }

            Settings.Size.Width = window.Width;
            Settings.Size.Height = window.Height;
            Settings.WindowState = window.WindowState;
            FileManager.SaveConfiguration(Settings);
            Logger.Information("The window is successfully closed");
        }
        catch (Exception exception)
        {
            Logger.Error(exception, "Error when closing the window");
        }
    }
}