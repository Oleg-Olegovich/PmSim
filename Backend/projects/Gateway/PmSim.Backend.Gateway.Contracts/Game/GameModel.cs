using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game
{
    public class GameModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
    }
}