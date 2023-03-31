using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

public class EmployeeWorkTask : EmployeeProjectTask
{
    public int ProgressPointNumber { get; }

    public EmployeeWorkTask(Player player, Project project, int progressPointNumber) 
        : base(player, project)
    {
        ProgressPointNumber = progressPointNumber;
    }
}