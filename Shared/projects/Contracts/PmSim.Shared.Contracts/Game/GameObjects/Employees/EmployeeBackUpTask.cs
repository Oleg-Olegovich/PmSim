using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

public class EmployeeBackUpTask : EmployeeProjectTask
{
    public int OfficeId { get; }

    public EmployeeBackUpTask(Player player, Project project, int officeId) 
        : base(player, project)
    {
        OfficeId = officeId;
    }
}