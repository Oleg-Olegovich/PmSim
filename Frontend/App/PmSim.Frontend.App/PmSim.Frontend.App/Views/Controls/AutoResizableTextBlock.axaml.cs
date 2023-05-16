using Avalonia;
using Avalonia.Controls.Primitives;

namespace PmSim.Frontend.App.Views.Controls;

public class AutoResizableTextBlock : BasicCustomControl
{
    public static readonly StyledProperty<string?> TextProperty
        = AvaloniaProperty.Register<AutoResizableTextBlock, string?>(nameof(Text));

    public string? Text
    {
        get => GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
}