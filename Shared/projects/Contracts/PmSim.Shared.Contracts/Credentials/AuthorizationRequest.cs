using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Credentials
{
    public class AuthorizationRequest
    {
        //[Required]
        //[MaxLength(100)]
        [JsonPropertyName("username")]
        public string Username { get; set; }
        
        //[Required]
        //[MaxLength(100)]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}