using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game
{
    public class GameModel
    {
        [JsonProperty("id")] public int Id { get; set; }
    }
}