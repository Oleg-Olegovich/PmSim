using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game
{
    public class CreateGameRequest
    {
        //[Required]
        //[MaxLength(100)]
        [JsonPropertyName("founder")]
        public string Founder { get; set; }
        
        //[Required]
        [JsonPropertyName("maxPlayersNumber")]
        public int MaxPlayersNumber { get; set; }

        //[Required]
        [JsonPropertyName("botsNumber")]
        public int BotsNumber { get; set; }
        
        //[Required]
        [JsonPropertyName("mode")]
        public int Mode { get; set; }

        [JsonPropertyName("settings")]
        public Options Settings { get; set; }
    }
}