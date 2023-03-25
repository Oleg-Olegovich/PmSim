using System.Text.Json.Serialization;
using PmSim.Shared.Contracts.Game.GameObjects.Others;

namespace PmSim.Shared.Contracts.Game.Status
{
    public class IncidentGameStatusModel : GameStatusModel
    {
        [JsonPropertyName("incident")] public IncidentModel Incident { get; set; }
    }
}