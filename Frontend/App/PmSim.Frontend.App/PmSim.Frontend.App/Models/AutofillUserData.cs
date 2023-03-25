namespace PmSim.Frontend.App.Models;

/// <summary>
/// Stores forms autofill data.
/// </summary>
public class AutofillUserData
{
    /// <summary>
    /// The default data is loaded from the application configuration.
    /// </summary>
    public static AutofillUserData? Default { get; set; }
    
    public string? SingleLogin { get; set; }
    
    public string? Login { get; set; }
    
    public string? Password { get; set; }
    
    public bool IsSingleDataRemembered { get; set; }
    
    public bool IsMultiplayerDataRemembered { get; set; }
}