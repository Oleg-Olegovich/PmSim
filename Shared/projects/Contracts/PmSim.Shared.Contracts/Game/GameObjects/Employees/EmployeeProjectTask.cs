using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees
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