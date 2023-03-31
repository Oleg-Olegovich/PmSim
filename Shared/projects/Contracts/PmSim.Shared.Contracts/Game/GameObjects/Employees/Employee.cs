using PmSim.Shared.Contracts.Interfaces;
using PmSim.Shared.GameEngine.Dto;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

public class Employee : IAuctionLot
{
    public (int, int) NameNumbers { get; }

    public SkillsPoints Skills { get; }

    public int DesiredSalary
        => Math.Min(Math.Max(1,
            GameConstants.MaxSalary * (Skills.Programming + Skills.Music + Skills.Design + Skills.Management)
            / (4 * GameConstants.MaxSkillLevel) + (new Random()).Next(-1, 2)), GameConstants.MaxSalary);

    public int Salary { get; set; }

    public EmployeeTask? CurrentTask { get; set; }

    public Employee() 
        => Skills = new SkillsPoints(0, 0, 0, 0, 0);

    public Employee(SkillsPoints skills)
    {
        NameNumbers = (0, 0);
        Skills = skills;
    }

    public Employee((int, int) nameNumbers, SkillsPoints skills)
    {
        NameNumbers = nameNumbers;
        Skills = skills;
    }
}