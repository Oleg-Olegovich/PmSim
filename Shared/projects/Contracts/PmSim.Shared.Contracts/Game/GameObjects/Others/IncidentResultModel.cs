using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others
{
    public class IncidentResultModel : IncidentModel
    {
        [JsonPropertyName("donationsSum")] public int DonationsSum { get; set; }
    }
}