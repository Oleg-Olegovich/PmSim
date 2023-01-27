using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game
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

        //[Required]
        [JsonPropertyName("isDefaultSettings")]
        public bool IsDefaultSettings { get; set; }

        [JsonPropertyName("settings")]
        public SettingsModel Settings { get; set; }
    }
}