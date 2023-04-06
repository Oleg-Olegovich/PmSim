using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

public class EmployeeStatus : IAuctionLot
{
    public (int, int) NameNumbers { get; init; }
    
    public SkillsPoints Skills { get; init; }
    
    public int Salary { get; set; }
    
    public EmployeeTaskTypes TaskType { get; set; }
}