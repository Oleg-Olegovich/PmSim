using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

public class EmployeeDevelopmentTask : EmployeeProjectTask
{
    public int ProgressPointNumber { get; }

    public EmployeeDevelopmentTask(Player player, Project project, int progressPointNumber) 
        : base(player, project)
    {
        ProgressPointNumber = progressPointNumber;
    }
}