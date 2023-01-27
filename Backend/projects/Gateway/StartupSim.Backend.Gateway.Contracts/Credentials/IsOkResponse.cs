using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Credentials
{
    public class IsOkResponse
    {
        [JsonProperty("isOk")] 
        public bool IsOk { get; set; }
    }
}