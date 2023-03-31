using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Diplomacy;

public class IncidentModel
{
    [JsonPropertyName("descriptionNumber")] 
    public int DescriptionNumber { get; set; }

    [JsonPropertyName("passCost")] 
    public int PassCost { get; set; }
    
    [JsonPropertyName("donationsSum")] 
    public int DonationsSum { get; set; }
}