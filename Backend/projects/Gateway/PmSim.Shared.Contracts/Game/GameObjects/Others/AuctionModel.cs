using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others
{
    public class AuctionModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("lot")] public ILotModel Lot { get; set; }

        [JsonPropertyName("sellerId")] public int SellerId { get; set; }

        [JsonPropertyName("startPrice")] public int StartPrice { get; set; }

        [JsonPropertyName("lastBuyerId")] public int LastBuyerId { get; set; }

        [JsonPropertyName("lastPrice")] public int LastPrice { get; set; }

        [JsonPropertyName("isPublic")] public bool IsPublic { get; set; }
    }
}