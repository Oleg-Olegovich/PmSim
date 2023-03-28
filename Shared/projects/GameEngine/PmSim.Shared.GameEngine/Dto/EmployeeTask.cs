using PmSim.Shared.GameEngine.GameLogic;

namespace PmSim.Shared.GameEngine.Dto
{
    /// <summary>
    /// Base class or for assign to invent project.
    /// </summary>
    internal class EmployeeTask
    {
        internal Player Player { get; }

        internal EmployeeTask(Player player)
        {
            Player = player;
        }
    }
}