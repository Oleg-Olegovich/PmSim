using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

public class Employee : EmployeeStatus
{
    public int DesiredSalary
        => Math.Min(Math.Max(1,
            GameConstants.MaxSalary * (Skills.Programming + Skills.Music + Skills.Design + Skills.Management)
            / (4 * GameConstants.MaxSkillLevel) + (new Random()).Next(-1, 2)), GameConstants.MaxSalary);

    private EmployeeTask? _currentTask;

    public EmployeeTask? CurrentTask
    {
        get => _currentTask;
        set
        {
            _currentTask = value;
            TaskType = value switch
            {
                null => EmployeeTaskTypes.Non,
                EmployeeDevelopmentTask => EmployeeTaskTypes.Development,
                EmployeeBackUpTask => EmployeeTaskTypes.BackUp,
                _ => EmployeeTaskTypes.Invention
            };
        }
    }

    public Employee((int, int) nameNumbers, SkillsPoints skills)
    {
        NameNumbers = nameNumbers;
        Skills = skills;
    }
    
    protected Employee() 
        => Skills = new SkillsPoints(0, 0, 0, 0, 0);

    protected Employee(SkillsPoints skills)
    {
        NameNumbers = (0, 0);
        Skills = skills;
    }
}