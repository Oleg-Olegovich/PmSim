using Avalonia;
using Avalonia.Controls.Primitives;

namespace PmSim.Frontend.App.Views.Controls;

public class BasicCustomControl : TemplatedControl
{
    public static readonly StyledProperty<string?> ClassesProperty
        = AvaloniaProperty.Register<AutoResizableTextBlock, string?>(nameof(Classes));
    
    public new string? Classes
    {
        get => GetValue(ClassesProperty);
        set => SetValue(ClassesProperty, value);
    }
}