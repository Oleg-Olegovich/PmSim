using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

public class EmployeeBackUpTask : EmployeeProjectTask
{
    public int OfficeNumber { get; }

    public EmployeeBackUpTask(Player player, Project project, int officeNumber) 
        : base(player, project)
    {
        OfficeNumber = officeNumber;
    }
}