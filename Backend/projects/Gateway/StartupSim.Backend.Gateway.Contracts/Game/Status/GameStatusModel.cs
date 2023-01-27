using System;
using System.Linq;
using Newtonsoft.Json;
using StartupSim.Backend.GameEngine.Enums;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace StartupSim.Backend.Gateway.Contracts.Game.Status
{
    public class GameStatusModel
    {
        [JsonProperty("stage")] public GameStages Stage { get; set; }

        [JsonProperty("time")] public int Time { get; set; }

        [JsonProperty("offices")] public OfficeModel[] Offices { get; set; }

        [JsonProperty("actors")] public ActorModel[] Actors { get; set; }

        [JsonProperty("mapNumber")] public int MapNumber { get; set; }

        [JsonProperty("player")] public PlayerModel Player { get; set; }

        public override string ToString()
        {
            var result = "Stage: " + Stage + Environment.NewLine + "Time: "
                         + Time + Environment.NewLine + "Actors:" + Environment.NewLine;
            return Actors.Aggregate(result, (current, actor) => current + actor + Environment.NewLine);
        }
    }
}