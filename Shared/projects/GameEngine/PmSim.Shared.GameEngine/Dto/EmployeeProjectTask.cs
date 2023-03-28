using PmSim.Shared.GameEngine.GameLogic;

namespace PmSim.Shared.GameEngine.Dto
{
    internal abstract class EmployeeProjectTask : EmployeeTask
    {
        internal Project Project { get; }

        protected EmployeeProjectTask(Player player, Project project) : base(player)
        {
            Project = project;
        }
    }
}