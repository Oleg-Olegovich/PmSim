using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Shared.Contracts.Game.Employees;

public class EmployeeBackUpTask : EmployeeProjectTask
{
    public int OfficeId { get; }

    public EmployeeBackUpTask(Player player, Project project, int officeId) 
        : base(player, project)
    {
        OfficeId = officeId;
    }
}