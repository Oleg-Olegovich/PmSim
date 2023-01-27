using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Credentials
{
    public class IsOkResponse
    {
        [JsonPropertyName("isOk")] 
        public bool IsOk { get; set; }
    }
}