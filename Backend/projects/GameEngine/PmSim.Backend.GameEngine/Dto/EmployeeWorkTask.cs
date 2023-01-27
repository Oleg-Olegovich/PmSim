using PmSim.Backend.GameEngine.GameLogic;

namespace PmSim.Backend.GameEngine.Dto
{
    public class EmployeeWorkTask : EmployeeProjectTask
    {
        public int ProgressPointNumber { get; }

        public EmployeeWorkTask(Player player, Project project, int progressPointNumber) : base(player, project)
        {
            ProgressPointNumber = progressPointNumber;
        }
    }
}