using StartupSim.Backend.GameEngine.GameLogic;

namespace StartupSim.Backend.GameEngine.Dto
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