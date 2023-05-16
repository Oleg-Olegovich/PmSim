using System.Windows.Input;
using Avalonia;

namespace PmSim.Frontend.App.Views.Controls;

public class AutoResizableButton : BasicCustomControl
{
    public static readonly StyledProperty<object?> ContentProperty
        = AvaloniaProperty.Register<AutoResizableTextBlock, object?>(nameof(Content));
    
    public static readonly StyledProperty<ICommand?> CommandProperty
        = AvaloniaProperty.Register<AutoResizableTextBlock, ICommand?>(nameof(Command));

    public object? Content
    {
        get => GetValue(ContentProperty);
        set => SetValue(ContentProperty, value);
    }
    
    public ICommand? Command
    {
        get => GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }
}