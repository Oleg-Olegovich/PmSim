using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game
{
    public class GameModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }
    }
}