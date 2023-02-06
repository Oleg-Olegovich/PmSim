using System;
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PmSim.Frontend.App.Views.Windows;

/// <summary>
/// Adds custom properties to the Window control.
/// </summary>
public abstract class BasicWindowView : Window
{
    public static readonly AttachedProperty<EventHandler<WindowClosingEventArgs>?> ClosingHandlerProperty
        = AvaloniaProperty
            .RegisterAttached<BasicWindowView, Window, EventHandler<WindowClosingEventArgs>?>("ClosingHandler");

    static BasicWindowView()
    {
        ClosingHandlerProperty.Changed.Subscribe(
            changedEvent => HandleClosingHandlerChanged(changedEvent.Sender, 
                changedEvent.NewValue.GetValueOrDefault<EventHandler<WindowClosingEventArgs>?>()));
    }
    
    public static EventHandler<WindowClosingEventArgs>? GetClosingHandler(Window window)
        => window.GetValue(ClosingHandlerProperty);

    public static void SetClosingHandler(Window window, EventHandler<WindowClosingEventArgs>? handler)
        => window.SetValue(ClosingHandlerProperty, handler);

    private static void HandleClosingHandlerChanged(AvaloniaObject element, EventHandler<WindowClosingEventArgs>? handler)
    {
        if (element is Window window && handler != null)
        {
            window.Closing += handler;
        }
    }

    public Action<Window> ChangingWindowStateHandler { get; set; }
        = _ => { };

    protected BasicWindowView()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }

    protected virtual void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    protected override void HandleWindowStateChanged(WindowState state)
    { }
}