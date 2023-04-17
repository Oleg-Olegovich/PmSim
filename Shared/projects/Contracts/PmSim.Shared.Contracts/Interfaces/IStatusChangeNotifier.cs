using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Shared.Contracts.Interfaces;

/// <summary>
/// It is used in the Game class from GameEngine to change the status on the client
/// (synchronization). In single-player mode, GameEngine interacts directly with PmSimClient,
/// and in multiplayer mode - through Gateway. Therefore, an interface is needed.
/// </summary>
public interface IStatusChangeNotifier
{
    public int ActionsNumber { set; }
    
    public int Money { set; }
    
    public int OfficesNumber { set; }
    
    public int TechSupportOfficersNumber { set; }

    public int MaxEmployeesNumber { set; }
    
    public int Programming { set; }
    
    public int Music { set; }
    
    public int Design { set; }
    
    public int Management { set; }
    
    public int Creativity { set; }
    
    public bool IsOut { set; }

    public List<PlayerStatus> Players { set; }

    public void SetAnotherStatus(PlayerStatus player);

    public void RemovePlayer(int id);

    public void Add(EmployeeStatus employee);

    public void Remove(EmployeeStatus employee);

    public void Add(Project project);
    
    public void StartProject(int id);

    public void CompleteProject(int id);

    public void FailProject(int id);

    public void UpdateProjectProgress(int id, ProgressPoints points);

    public void AddOpportunity(int number);

    public void RemoveOpportunity(int number);

    public Task ChangeCurrentStageAsync(GameStages stage, int time);

    public void ChangeOfficeState(int officeId, OfficeStates officeState);
}