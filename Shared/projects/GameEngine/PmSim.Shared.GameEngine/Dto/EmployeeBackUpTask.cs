using PmSim.Shared.GameEngine.GameLogic;

namespace PmSim.Shared.GameEngine.Dto
{
    internal class EmployeeBackUpTask : EmployeeProjectTask
    {
        internal int OfficeNumber { get; }

        internal EmployeeBackUpTask(Player player, Project project, int officeNumber) : base(player, project)
        {
            OfficeNumber = officeNumber;
        }
    }
}