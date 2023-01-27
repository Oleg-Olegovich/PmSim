using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class IncidentResultModel : IncidentModel
    {
        [JsonProperty("donationsSum")] public int DonationsSum { get; set; }
    }
}