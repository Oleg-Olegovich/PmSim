using System.Text.Json.Serialization;
using PmSim.Shared.Contracts.Credentials;
using PmSim.Shared.Contracts.Game;

namespace PmSim.Shared.Contracts.Actions;

public class GameCreationRequest : BasicRequest
{
    [JsonPropertyName("options")] 
    public GameOptions? Options { get; set; }
}