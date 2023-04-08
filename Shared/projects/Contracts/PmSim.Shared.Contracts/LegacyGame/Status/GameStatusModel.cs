using System;
using System.Linq;
using System.Text.Json.Serialization;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Others;

namespace PmSim.Shared.Contracts.Game.Status
{
    public class GameStatusModel
    {
        [JsonPropertyName("stage")] public GameStages Stage { get; set; }

        [JsonPropertyName("time")] public int Time { get; set; }

        //[JsonPropertyName("offices")] public OfficeModel[] Offices { get; set; }

        //[JsonPropertyName("actors")] public ActorModel[] Actors { get; set; }

        [JsonPropertyName("mapNumber")] public int MapNumber { get; set; }

        //[JsonPropertyName("player")] public PlayerModel Player { get; set; }
    }
}