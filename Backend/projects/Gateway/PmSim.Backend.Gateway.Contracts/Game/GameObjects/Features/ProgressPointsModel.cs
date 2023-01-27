using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class ProgressPointsModel
    {
        [JsonPropertyName("programming")] public int Programming { get; set; }

        [JsonPropertyName("music")] public int Music { get; set; }

        [JsonPropertyName("design")] public int Design { get; set; }

        [JsonPropertyName("management")] public int Management { get; set; }
    }
}