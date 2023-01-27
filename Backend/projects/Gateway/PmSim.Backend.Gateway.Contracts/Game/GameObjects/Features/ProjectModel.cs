using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Features
{
    public class ProjectModel : FeatureModel, ILotModel
    {
        [JsonPropertyName("features")] public FeatureModel[] Features { get; set; }

        [JsonPropertyName("deadline")] public int Deadline { get; set; }

        [JsonPropertyName("isFailed")] public bool IsFailed { get; set; }
    }
}