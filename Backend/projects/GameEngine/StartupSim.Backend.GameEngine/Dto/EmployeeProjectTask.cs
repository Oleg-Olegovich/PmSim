using StartupSim.Backend.GameEngine.GameLogic;

namespace StartupSim.Backend.GameEngine.Dto
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