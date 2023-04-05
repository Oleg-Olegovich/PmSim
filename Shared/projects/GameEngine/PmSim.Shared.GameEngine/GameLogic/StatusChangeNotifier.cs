using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.GameEngine.GameLogic;

/// <summary>
/// A stub for bots.
/// </summary>
internal class StatusChangeNotifier : IStatusChangeNotifier
{
    public int ActionsNumber { get; set; }
    public int Money { get; set; }
    public int OfficesNumber { get; set; }
    public int ProjectsNumber { get; set; }
    public int EmployeesNumber { get; set; }
    public int MaxEmployeesNumber { get; set; }
    public int Programming { get; set; }
    public int Music { get; set; }
    public int Design { get; set; }
    public int Management { get; set; }
    public int Creativity { get; set; }
    
    public PlayerStatus AnotherPlayerStatus { get; set; }
    public IEnumerable<Office> Offices { get; set; }
    public Office OfficeStatus { get; set; }
    public IEnumerable<PlayerStatus> Players { get; set; }

    public async Task ChangeCurrentStageAsync(GameStages stage, int time)
    {
    }

    public void ChangeOfficeState(int officeId, OfficeStates officeState)
    {
    }
}