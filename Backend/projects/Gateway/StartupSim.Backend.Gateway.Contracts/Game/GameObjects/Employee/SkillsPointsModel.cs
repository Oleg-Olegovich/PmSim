using Newtonsoft.Json;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Features;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Employee
{
    public class SkillsPointsModel : ProgressPointsModel
    {
        [JsonProperty("creativity")] public int Creativity { get; set; }
    }
}