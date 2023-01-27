using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Credentials
{
    public class AuthorizationRequest
    {
        //[Required]
        //[MaxLength(100)]
        [JsonProperty("username")]
        public string Username { get; set; }
        
        //[Required]
        //[MaxLength(100)]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}