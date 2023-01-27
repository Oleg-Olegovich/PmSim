using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class FeatureModel
    {
        [JsonPropertyName("nameNumber")] public int NameNumber { get; set; }

        [JsonPropertyName("points")] public ProgressPointsModel Points { get; set; }

        [JsonPropertyName("reward")] public RewardModel Reward { get; set; }

        [JsonPropertyName("isDone")] public bool IsDone { get; set; }
    }
}