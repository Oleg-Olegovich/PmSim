using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace PmSim.Backend.Gateway.Contracts.Game.Status
{
    public class IncidentGameStatusModel : GameStatusModel
    {
        [JsonPropertyName("incident")] public IncidentModel Incident { get; set; }
    }
}