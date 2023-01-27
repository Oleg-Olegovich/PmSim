using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class RewardModel
    {
        [JsonPropertyName("prize")] public int Prize { get; set; }

        [JsonPropertyName("revenue")] public int Revenue { get; set; }

        [JsonPropertyName("opportunities")] public int Opportunities { get; set; }
    }
}