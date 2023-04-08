using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Shared.Contracts.Game.Employees
{
    public abstract class EmployeeProjectTask : EmployeeTask
    {
        public Project Project { get; }

        protected EmployeeProjectTask(Player player, Project project) 
            : base(player)
        {
            Project = project;
        }
    }
}