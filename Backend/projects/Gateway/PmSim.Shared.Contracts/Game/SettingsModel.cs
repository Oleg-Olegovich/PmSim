using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game
{
    public class SettingsModel
    {
        [Required]
        [JsonPropertyName("connectionRealTime")] 
        public int ConnectionRealTime { get; set; }

        [Required]
        [JsonPropertyName("choosingBackgroundRealTime")]
        public int ChoosingBackgroundRealTime { get; set; }

        [Required]
        [JsonPropertyName("sprintRealTime")] 
        public int SprintRealTime { get; set; }

        [Required]
        [JsonPropertyName("diplomacyRealTime")] 
        public int DiplomacyRealTime { get; set; }

        [Required]
        [JsonPropertyName("incidentRealTime")] 
        public int IncidentRealTime { get; set; }

        [Required]
        [JsonPropertyName("sprintActionsNumbers")] 
        public int SprintActionsNumbers { get; set; }

        [Required]
        [JsonPropertyName("sprintActionsNumbers")] 
        public int AuctionRealTime { get; set; }

        [Required]
        [JsonPropertyName("sprintActionsNumbers")] 
        public int StartUpCapital { get; set; }
        
        [Required]
        [JsonPropertyName("mapNumber")] 
        public int MapNumber { get; set; }
    }
}