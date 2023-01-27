using Newtonsoft.Json;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace StartupSim.Backend.Gateway.Contracts.Game.Status
{
    public class DiplomacyGameStatusModel : GameStatusModel
    {
        [JsonProperty("auctions")] public AuctionModel[] Auctions { get; set; }
    }
}