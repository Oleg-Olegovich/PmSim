using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    public class AuctionModel
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("lot")] public ILotModel Lot { get; set; }

        [JsonProperty("sellerId")] public int SellerId { get; set; }

        [JsonProperty("startPrice")] public int StartPrice { get; set; }

        [JsonProperty("lastBuyerId")] public int LastBuyerId { get; set; }

        [JsonProperty("lastPrice")] public int LastPrice { get; set; }

        [JsonProperty("isPublic")] public bool IsPublic { get; set; }
    }
}