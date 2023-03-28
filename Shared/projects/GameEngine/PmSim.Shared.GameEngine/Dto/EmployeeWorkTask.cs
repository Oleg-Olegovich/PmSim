using PmSim.Shared.GameEngine.GameLogic;

namespace PmSim.Shared.GameEngine.Dto
{
    internal class EmployeeWorkTask : EmployeeProjectTask
    {
        internal int ProgressPointNumber { get; }

        internal EmployeeWorkTask(Player player, Project project, int progressPointNumber) : base(player, project)
        {
            ProgressPointNumber = progressPointNumber;
        }
    }
}