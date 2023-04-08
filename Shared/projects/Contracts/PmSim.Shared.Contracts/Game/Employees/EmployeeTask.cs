using PmSim.Shared.Contracts.Game.Others;

namespace PmSim.Shared.Contracts.Game.Employees;

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