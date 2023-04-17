using System.Collections.ObjectModel;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Shared.Contracts.Interfaces;

/// <summary>
/// The interface provides external control over the state
/// of the game screen. Used in the Client to notify the
/// player of changes when notified from the server.
/// </summary>
public interface IGameScreenLogic
{
    /// <summary>
    /// Current game stage.
    /// </summary>
    public GameStages GameStage { get; set; }
    
    /// <summary>
    /// Localized name of the game stage.
    /// </summary>
    public string GameStageName { get; set; }
    
    /// <summary>
    /// Time to the end of the stage in the format MM:SS.
    /// </summary>
    public string Time { get; set; }
    
    /// <summary>
    /// The number of actions available to the player.
    /// </summary>
    public int ActionsNumber { get; set; }
    
    public int Money { get; set; }
    
    public int OfficesNumber { get; set; }
    
    public int TechSupportOfficersNumber { get; set; }

    public int MaxEmployeesNumber { get; set; }
    
    public ObservableCollection<PlayerStatus> Players { get; set; }

    public void SetOfficeState(int officeId, OfficeStates officeState);

    public void Add(EmployeeStatus employee);

    public void Remove(EmployeeStatus employee);

    public void Add(Project project);
    
    public void StartProject(int id);

    public void CompleteProject(int id);

    public void FailProject(int id);

    public void UpdateProjectProgress(int id, ProgressPoints points);
    
    public void AddOpportunity(OpportunityModel opportunity);

    public void RemoveOpportunity(int number);

    public void ProcessLosing();
}