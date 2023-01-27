using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class OpportunityModel : ILotModel
    {
        [JsonPropertyName("descriptionNumber")] public int DescriptionNumber { get; set; }
    }
}