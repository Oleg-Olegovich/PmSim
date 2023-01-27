using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class ProgressPointsModel
    {
        [JsonProperty("programming")] public int Programming { get; set; }

        [JsonProperty("music")] public int Music { get; set; }

        [JsonProperty("design")] public int Design { get; set; }

        [JsonProperty("management")] public int Management { get; set; }
    }
}