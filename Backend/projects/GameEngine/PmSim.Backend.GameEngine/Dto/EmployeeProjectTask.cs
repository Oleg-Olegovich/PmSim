using PmSim.Backend.GameEngine.GameLogic;

namespace PmSim.Backend.GameEngine.Dto
{
    public abstract class EmployeeProjectTask : EmployeeTask
    {
        public Project Project { get; }

        protected EmployeeProjectTask(Player player, Project project) : base(player)
        {
            Project = project;
        }
    }
}