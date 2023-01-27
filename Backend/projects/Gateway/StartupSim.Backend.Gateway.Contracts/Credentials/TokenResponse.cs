using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Credentials
{
    public class TokenResponse
    {
        [JsonProperty("access_token")] 
        public string AccessToken { get; set; }
    }
}