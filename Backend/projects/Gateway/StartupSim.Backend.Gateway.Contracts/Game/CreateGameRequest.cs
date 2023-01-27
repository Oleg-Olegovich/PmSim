using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game
{
    public class CreateGameRequest
    {
        //[Required]
        //[MaxLength(100)]
        [JsonProperty("founder")]
        public string Founder { get; set; }
        
        //[Required]
        [JsonProperty("maxPlayersNumber")]
        public int MaxPlayersNumber { get; set; }

        //[Required]
        [JsonProperty("botsNumber")]
        public int BotsNumber { get; set; }
        
        //[Required]
        [JsonProperty("mode")]
        public int Mode { get; set; }

        //[Required]
        [JsonProperty("isDefaultSettings")]
        public bool IsDefaultSettings { get; set; }

        [JsonProperty("settings")]
        public SettingsModel Settings { get; set; }
    }
}