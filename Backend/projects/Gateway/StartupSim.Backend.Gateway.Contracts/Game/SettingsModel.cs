using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game
{
    public class SettingsModel
    {
        //[Required]
        [JsonProperty("connectionRealTime")] 
        public int ConnectionRealTime { get; set; }

        //[Required]
        [JsonProperty("choosingBackgroundRealTime")]
        public int ChoosingBackgroundRealTime { get; set; }

        //[Required]
        [JsonProperty("sprintRealTime")] 
        public int SprintRealTime { get; set; }

        //[Required]
        [JsonProperty("diplomacyRealTime")] 
        public int DiplomacyRealTime { get; set; }

        //[Required]
        [JsonProperty("incidentRealTime")] 
        public int IncidentRealTime { get; set; }

        //[Required]
        [JsonProperty("sprintActionsNumbers")] 
        public int SprintActionsNumbers { get; set; }

        //[Required]
        [JsonProperty("sprintActionsNumbers")] 
        public int AuctionRealTime { get; set; }

        //[Required]
        [JsonProperty("sprintActionsNumbers")] 
        public int StartUpCapital { get; set; }
        
        //[Required]
        [JsonProperty("mapNumber")] 
        public int MapNumber { get; set; }
    }
}