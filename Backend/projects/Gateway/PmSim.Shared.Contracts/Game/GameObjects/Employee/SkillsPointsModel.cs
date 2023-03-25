using System.Text.Json.Serialization;
using PmSim.Shared.Contracts.Game.GameObjects.Features;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employee
{
    public class SkillsPointsModel : ProgressPointsModel
    {
        [JsonPropertyName("creativity")] public int Creativity { get; set; }
    }
}