using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Shared.Contracts.Game.Employees;

public class EmployeeBackUpTask : EmployeeProjectTask
{
    public EmployeeBackUpTask(Player player, Project project) 
        : base(player, project)
    { }
}