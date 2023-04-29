using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Credentials;

public class BasicRequest
{
    [JsonPropertyName("access_token")] 
    public string? AccessToken { get; set; }
    
    [JsonPropertyName("login")] 
    public int UserId { get; set; }
}