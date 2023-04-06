using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;
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
    public int TechSupportOfficersNumber { get; set; }
    public int ProjectsNumber { get; set; }
    public int CompletedProjectsNumber { get; set; }
    public int FailedProjectsNumber { get; set; }
    public int EmployeesNumber { get; set; }
    public int MaxEmployeesNumber { get; set; }
    public int Programming { get; set; }
    public int Music { get; set; }
    public int Design { get; set; }
    public int Management { get; set; }
    public int Creativity { get; set; }
    public IEnumerable<PlayerStatus> Players { get; set; }
    public PlayerStatus AnotherPlayerStatus { get; set; }
    public int RemovePlayer { get; set; }
    public EmployeeStatus AddEmployee { get; set; }
    public EmployeeStatus RemoveEmployee { get; set; }
    public Project Project { get; set; }
    public int AddOpportunity { get; set; }
    public int RemoveOpportunity { get; set; }
    public async Task ChangeCurrentStageAsync(GameStages stage, int time)
    {
        
    }

    public void ChangeOfficeState(int officeId, OfficeStates officeState)
    {
        
    }
}