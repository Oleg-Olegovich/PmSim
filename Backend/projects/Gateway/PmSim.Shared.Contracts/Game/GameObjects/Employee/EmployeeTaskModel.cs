using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employee
{
    public class EmployeeTaskModel
    {
        /// <summary>
        /// The player for whom the employee will work (there may be staff leasing).
        /// </summary>
        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("type")] public EmployeeTaskModelTypes Type { get; set; }

        /// <summary>
        /// The number of object to which the task is applied:
        /// -1 (if an inventing), an office or a feature.
        /// </summary>
        [JsonPropertyName("taskObject")]
        public int TaskObject { get; set; }
    }
}