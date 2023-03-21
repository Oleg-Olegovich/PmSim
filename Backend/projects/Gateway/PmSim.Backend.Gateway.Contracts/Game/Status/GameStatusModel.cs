using System.Text.Json.Serialization;
using PmSim.Backend.Gateway.Contracts.Enums;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace PmSim.Backend.Gateway.Contracts.Game.Status
{
    public class GameStatusModel
    {
        [JsonPropertyName("stage")] public GameStages Stage { get; set; }

        [JsonPropertyName("time")] public int Time { get; set; }

        [JsonPropertyName("offices")] public OfficeModel[] Offices { get; set; }

        [JsonPropertyName("actors")] public ActorModel[] Actors { get; set; }

        [JsonPropertyName("mapNumber")] public int MapNumber { get; set; }

        [JsonPropertyName("player")] public PlayerModel Player { get; set; }

        public override string ToString()
        {
            var result = "Stage: " + Stage + Environment.NewLine + "Time: "
                         + Time + Environment.NewLine + "Actors:" + Environment.NewLine;
            return Actors.Aggregate(result, (current, actor) => current + actor + Environment.NewLine);
        }
    }
}