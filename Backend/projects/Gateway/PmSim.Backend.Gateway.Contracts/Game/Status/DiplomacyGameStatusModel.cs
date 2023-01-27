using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace PmSim.Backend.Gateway.Contracts.Game.Status
{
    public class DiplomacyGameStatusModel : GameStatusModel
    {
        [JsonPropertyName("auctions")] public AuctionModel[] Auctions { get; set; }
    }
}