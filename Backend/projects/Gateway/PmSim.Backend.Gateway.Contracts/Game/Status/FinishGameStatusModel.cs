using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;
using PmSim.Backend.GameEngine.GameLogic;

namespace PmSim.Backend.Gateway.Contracts.Game.Status
{
    public class FinishGameStatusModel : GameStatusModel
    {
        [JsonPropertyName("winner")] public ActorModel Winner { get; set; }
    }
}