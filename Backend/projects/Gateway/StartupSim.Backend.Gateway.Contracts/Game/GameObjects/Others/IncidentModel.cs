using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class IncidentModel
    {
        [JsonProperty("descriptionNumber")] public int DescriptionNumber { get; set; }

        [JsonProperty("passCost")] public int PassCost { get; set; }
    }
}