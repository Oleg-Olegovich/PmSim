using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;
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
    public int CurrentProjectsNumber { get; set; }
    public int IdeasNumber { get; set; }
    public int CompletedProjectsNumber { get; set; }
    public int FailedProjectsNumber { get; set; }
    public int EmployeesNumber { get; set; }
    public int MaxEmployeesNumber { get; set; }
    public int Programming { get; set; }
    public int Music { get; set; }
    public int Design { get; set; }
    public int Management { get; set; }
    public int Creativity { get; set; }
    public List<PlayerStatus> Players { get; set; }
    public void SetAnotherStatus(PlayerStatus player)
    {
        
    }

    public void RemovePlayer(int id)
    {
        
    }

    public void Add(EmployeeStatus employee)
    {
        
    }

    public void Remove(EmployeeStatus employee)
    {
        
    }

    public void Add(Project project)
    {
        
    }

    public void StartProject(int id)
    {
        
    }

    public void CompleteProject(int id)
    {
        
    }

    public void FailProject(int id)
    {
        
    }

    public void UpdateProjectProgress(int id, ProgressPoints points)
    {
        
    }

    public void UpdateProject(int id, Project project)
    {
        
    }

    public void AddOpportunity(int number)
    {
        
    }

    public void RemoveOpportunity(int number)
    {
        
    }

    public bool IsOut { get; set; }
    
    public async Task ChangeCurrentStageAsync(GameStages stage, int time)
    {
        
    }

    public void ChangeOfficeState(int officeId, OfficeStates officeState)
    {
        
    }
}