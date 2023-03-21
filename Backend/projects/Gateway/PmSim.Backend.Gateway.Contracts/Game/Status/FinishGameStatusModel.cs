using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace PmSim.Backend.Gateway.Contracts.Game.Status
{
    public class FinishGameStatusModel : GameStatusModel
    {
        [JsonPropertyName("winner")] public ActorModel Winner { get; set; }
    }
}