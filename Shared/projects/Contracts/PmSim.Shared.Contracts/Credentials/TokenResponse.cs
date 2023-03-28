using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Credentials
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")] 
        public string AccessToken { get; set; }
    }
}