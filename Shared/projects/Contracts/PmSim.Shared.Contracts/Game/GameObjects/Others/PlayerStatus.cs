using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others;

/// <summary>
/// Players or bots status (property, money).
/// </summary>
public class PlayerStatus
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("name")] 
    public string Name { get; set; }

    [JsonPropertyName("money")] 
    public int Money { get; set; }

    [JsonPropertyName("completedProjects")] 
    public int CompletedProjects { get; set; }
}