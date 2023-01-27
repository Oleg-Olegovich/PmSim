using Newtonsoft.Json;
using StartupSim.Backend.GameEngine.GameLogic;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace StartupSim.Backend.Gateway.Contracts.Game.Status
{
    public class FinishGameStatusModel : GameStatusModel
    {
        [JsonProperty("winner")] public ActorModel Winner { get; set; }
    }
}