using System;
using System.Text.Json.Serialization;

namespace PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    /// <summary>
    /// Players or bots status (property, money).
    /// </summary>
    public class ActorModel
    {
        [JsonPropertyName("id")] public int Id { get; set; }

        [JsonPropertyName("name")] public string Name { get; set; }

        [JsonPropertyName("money")] public int Money { get; set; }

        [JsonPropertyName("completedProjects")] public int CompletedProjects { get; set; }

        public override string ToString()
            => Name + Environment.NewLine + Money + Environment.NewLine + CompletedProjects;
    }
}