using Newtonsoft.Json;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Features;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class PlayerModel : ActorModel
    {
        [JsonProperty("isStartupOpen")] public bool IsStartupOpen { get; set; }

        [JsonProperty("isOut")] public bool IsOut { get; set; }

        [JsonProperty("actionsNumber")] public int ActionsNumber { get; set; }

        [JsonProperty("projects")] public ProjectModel[] Projects { get; set; }

        [JsonProperty("opportunities")] public int[] Opportunities { get; set; }
    }
}