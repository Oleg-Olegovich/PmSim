using Newtonsoft.Json;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Employee
{
    public class EmployeeModel : ILotModel
    {
        [JsonProperty("nameNumber")] public (int, int) NameNumbers { get; set; }

        [JsonProperty("skills")] public SkillsPointsModel Skills { get; set; }

        [JsonProperty("desiredSalary")] public int DesiredSalary { get; set; }

        [JsonProperty("salary")] public int Salary { get; set; }

        [JsonProperty("taskShortDescription")] public EmployeeTaskModel TaskShortDescription { get; set; }
    }
}