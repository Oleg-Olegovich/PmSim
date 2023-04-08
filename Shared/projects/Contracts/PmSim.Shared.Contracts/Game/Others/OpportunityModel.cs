using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.Others;

public class OpportunityModel
{
    [JsonPropertyName("descriptionNumber")] 
    public int DescriptionNumber { get; set; }
}