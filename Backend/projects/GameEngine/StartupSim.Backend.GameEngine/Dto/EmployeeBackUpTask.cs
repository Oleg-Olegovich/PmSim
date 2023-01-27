using StartupSim.Backend.GameEngine.GameLogic;

namespace StartupSim.Backend.GameEngine.Dto
{
    public class EmployeeBackUpTask : EmployeeProjectTask
    {
        public int OfficeNumber { get; }

        public EmployeeBackUpTask(Player player, Project project, int officeNumber) : base(player, project)
        {
            OfficeNumber = officeNumber;
        }
    }
}