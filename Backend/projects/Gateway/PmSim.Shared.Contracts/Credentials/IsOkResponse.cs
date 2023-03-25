using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Credentials
{
    public class IsOkResponse
    {
        [JsonPropertyName("isOk")] 
        public bool IsOk { get; set; }
    }
}