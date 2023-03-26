namespace PmSim.Frontend.App.Models;

/// <summary>
/// Stores forms autofill data.
/// </summary>
public class AutofillUserData
{
    public string? SingleLogin { get; set; }
    
    public string? Login { get; set; }
    
    public string? Password { get; set; }
    
    public bool IsSingleDataRemembered { get; set; }
    
    public bool IsMultiplayerDataRemembered { get; set; }
}