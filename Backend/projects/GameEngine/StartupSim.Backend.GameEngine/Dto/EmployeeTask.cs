using StartupSim.Backend.GameEngine.GameLogic;

namespace StartupSim.Backend.GameEngine.Dto
{
    /// <summary>
    /// Base class or for assign to invent project.
    /// </summary>
    public class EmployeeTask
    {
        public Player Player { get; }

        public EmployeeTask(Player player)
        {
            Player = player;
        }
    }
}