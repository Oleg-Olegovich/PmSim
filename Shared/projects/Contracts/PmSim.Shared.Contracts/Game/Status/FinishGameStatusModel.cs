using System.Text.Json.Serialization;
using PmSim.Shared.Contracts.Game.GameObjects.Others;

namespace PmSim.Shared.Contracts.Game.Status
{
    public class FinishGameStatusModel : GameStatusModel
    {
        [JsonPropertyName("winner")] public ActorModel Winner { get; set; }
    }
}