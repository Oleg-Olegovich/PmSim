using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class OpportunityModel : ILotModel
    {
        [JsonProperty("descriptionNumber")] public int DescriptionNumber { get; set; }
    }
}