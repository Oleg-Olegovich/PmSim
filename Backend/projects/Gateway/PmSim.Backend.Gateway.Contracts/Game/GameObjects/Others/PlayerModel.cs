using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Features;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class PlayerModel : ActorModel
    {
        [JsonPropertyName("isStartupOpen")] public bool IsStartupOpen { get; set; }

        [JsonPropertyName("isOut")] public bool IsOut { get; set; }

        [JsonPropertyName("actionsNumber")] public int ActionsNumber { get; set; }

        [JsonPropertyName("projects")] public ProjectModel[] Projects { get; set; }

        [JsonPropertyName("opportunities")] public int[] Opportunities { get; set; }
    }
}