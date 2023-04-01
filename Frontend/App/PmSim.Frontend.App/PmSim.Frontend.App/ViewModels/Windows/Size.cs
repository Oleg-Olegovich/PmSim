namespace PmSim.Frontend.App.ViewModels.Windows;

/// <summary>
/// Window size.
/// </summary>
public class Size
{
    /// <summary>
    /// The default sizes are loaded from the application configuration.
    /// </summary>
    public static Size Default => new()
    {
        Width = 600, 
        Height = 400
    };

    public double Width { get; set; }

    public double Height { get; set; }
}