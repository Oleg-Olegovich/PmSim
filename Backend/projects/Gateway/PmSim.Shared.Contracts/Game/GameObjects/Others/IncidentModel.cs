using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others
{
    public class IncidentModel
    {
        [JsonPropertyName("descriptionNumber")] public int DescriptionNumber { get; set; }

        [JsonPropertyName("passCost")] public int PassCost { get; set; }
    }
}