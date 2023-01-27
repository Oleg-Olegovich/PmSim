using System;
using Newtonsoft.Json;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others
{
    /// <summary>
    /// Players or bots status (property, money).
    /// </summary>
    public class ActorModel
    {
        [JsonProperty("id")] public int Id { get; set; }

        [JsonProperty("name")] public string Name { get; set; }

        [JsonProperty("money")] public int Money { get; set; }

        [JsonProperty("completedProjects")] public int CompletedProjects { get; set; }

        public override string ToString()
            => Name + Environment.NewLine + Money + Environment.NewLine + CompletedProjects;
    }
}