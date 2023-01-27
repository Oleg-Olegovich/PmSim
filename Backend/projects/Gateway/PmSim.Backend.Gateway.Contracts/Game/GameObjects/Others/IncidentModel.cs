using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class IncidentModel
    {
        [JsonPropertyName("descriptionNumber")] public int DescriptionNumber { get; set; }

        [JsonPropertyName("passCost")] public int PassCost { get; set; }
    }
}