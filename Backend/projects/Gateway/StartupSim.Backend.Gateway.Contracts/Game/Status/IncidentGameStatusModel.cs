using Newtonsoft.Json;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace StartupSim.Backend.Gateway.Contracts.Game.Status
{
    public class IncidentGameStatusModel : GameStatusModel
    {
        [JsonProperty("incident")] public IncidentModel Incident { get; set; }
    }
}