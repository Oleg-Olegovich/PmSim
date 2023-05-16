using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace PmSim.Frontend.App.Views.Controls;

public class BindableStyleClasses
{
    public static readonly AttachedProperty<string?> ClassesProperty 
        = AvaloniaProperty.RegisterAttached<BindableStyleClasses, StyledElement, string?>
            ("Classes", default, false, BindingMode.OneTime);
    
    static BindableStyleClasses()
    {
        ClassesProperty.Changed.Subscribe(
            changedEvent => HandleClassesChanged(changedEvent.Sender, 
                changedEvent.NewValue.GetValueOrDefault<string?>()));
    }

    public static string? GetClasses(AvaloniaObject element)
    {
        return element.GetValue(ClassesProperty);
    }
    
    public static void SetClasses(AvaloniaObject element, string? classesValue)
    {
        element.SetValue(ClassesProperty, classesValue);
    }

    private static void HandleClassesChanged(AvaloniaObject element, string? classes)
    {
        if (element is StyledElement styled)
        {
            styled.Classes = Classes.Parse(classes ?? "");
        }
    }
}