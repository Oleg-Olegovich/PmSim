using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Features;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Employee
{
    public class SkillsPointsModel : ProgressPointsModel
    {
        [JsonPropertyName("creativity")] public int Creativity { get; set; }
    }
}