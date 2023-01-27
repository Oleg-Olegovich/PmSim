using Newtonsoft.Json;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class ProjectModel : FeatureModel, ILotModel
    {
        [JsonProperty("features")] public FeatureModel[] Features { get; set; }

        [JsonProperty("deadline")] public int Deadline { get; set; }

        [JsonProperty("isFailed")] public bool IsFailed { get; set; }
    }
}