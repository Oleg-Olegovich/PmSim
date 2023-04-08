using System.Text.Json.Serialization;
using PmSim.Shared.Contracts.Game.Diplomacy;
using PmSim.Shared.Contracts.Game.Others;

namespace PmSim.Shared.Contracts.Game.Status
{
    public class IncidentGameStatusModel : GameStatusModel
    {
        [JsonPropertyName("incident")] public IncidentModel Incident { get; set; }
    }
}