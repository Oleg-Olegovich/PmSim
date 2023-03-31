using PmSim.Shared.Contracts.Game.GameObjects.Others;

namespace PmSim.Shared.Contracts.Game.GameObjects.Employees;

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