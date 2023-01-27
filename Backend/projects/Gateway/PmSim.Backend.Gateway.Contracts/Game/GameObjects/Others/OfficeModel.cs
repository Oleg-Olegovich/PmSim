using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class OfficeModel
    {
        [JsonPropertyName("ownerId")] public int OwnerId { get; set; }

        [JsonPropertyName("capacity")] public int Capacity { get; set; }

        [JsonPropertyName("rentalPrice")] public int RentalPrice { get; set; }
    }
}