using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class OfficeModel
    {
        [JsonProperty("ownerId")] public int OwnerId { get; set; }

        [JsonProperty("capacity")] public int Capacity { get; set; }

        [JsonProperty("rentalPrice")] public int RentalPrice { get; set; }
    }
}