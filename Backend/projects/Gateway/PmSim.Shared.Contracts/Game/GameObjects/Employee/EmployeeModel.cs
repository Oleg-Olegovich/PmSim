using System.Text.Json.Serialization;
using PmSim.Shared.Contracts.Game.GameObjects.Others;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employee
{
    public class EmployeeModel : ILotModel
    {
        [JsonPropertyName("nameNumber")] public (int, int) NameNumbers { get; set; }

        [JsonPropertyName("skills")] public SkillsPointsModel Skills { get; set; }

        [JsonPropertyName("desiredSalary")] public int DesiredSalary { get; set; }

        [JsonPropertyName("salary")] public int Salary { get; set; }

        [JsonPropertyName("taskShortDescription")] public EmployeeTaskModel TaskShortDescription { get; set; }
    }
}