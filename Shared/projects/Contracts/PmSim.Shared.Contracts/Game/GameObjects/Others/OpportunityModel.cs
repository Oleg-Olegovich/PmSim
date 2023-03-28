using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others
{
    public class OpportunityModel : ILotModel
    {
        [JsonPropertyName("descriptionNumber")] public int DescriptionNumber { get; set; }
    }
}