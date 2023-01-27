using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class FeatureModel
    {
        [JsonProperty("nameNumber")] public int NameNumber { get; set; }

        [JsonProperty("points")] public ProgressPointsModel Points { get; set; }

        [JsonProperty("reward")] public RewardModel Reward { get; set; }

        [JsonProperty("isDone")] public bool IsDone { get; set; }
    }
}