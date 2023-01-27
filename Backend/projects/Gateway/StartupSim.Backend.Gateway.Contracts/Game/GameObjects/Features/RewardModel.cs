using Newtonsoft.Json;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class RewardModel
    {
        [JsonProperty("prize")] public int Prize { get; set; }

        [JsonProperty("revenue")] public int Revenue { get; set; }

        [JsonProperty("opportunities")] public int Opportunities { get; set; }
    }
}