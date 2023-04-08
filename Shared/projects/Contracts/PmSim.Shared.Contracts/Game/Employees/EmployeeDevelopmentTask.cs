using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Shared.Contracts.Game.Employees;

public class EmployeeDevelopmentTask : EmployeeProjectTask
{
    public int ProgressPointNumber { get; }

    public EmployeeDevelopmentTask(Player player, Project project, int progressPointNumber) 
        : base(player, project)
    {
        ProgressPointNumber = progressPointNumber;
    }
}