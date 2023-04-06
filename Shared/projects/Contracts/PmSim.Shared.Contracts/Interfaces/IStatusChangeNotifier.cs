﻿using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

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
    
    public int ProjectsNumber { set; }
    
    public int CompletedProjectsNumber { set; }
    
    public int FailedProjectsNumber { set; }
    
    public int EmployeesNumber { set; }
    
    public int MaxEmployeesNumber { set; }
    
    public int Programming { set; }
    
    public int Music { set; }
    
    public int Design { set; }
    
    public int Management { set; }
    
    public int Creativity { set; }

    public IEnumerable<PlayerStatus> Players { set; }
    
    public PlayerStatus AnotherPlayerStatus { set; }
    
    public int RemovePlayer { set; }
    
    public EmployeeStatus AddEmployee { set; }
    
    public EmployeeStatus RemoveEmployee { set; }
    
    public Project Project { set; }
    
    public int AddOpportunity { set; }
    
    public int RemoveOpportunity { set; }

    public Task ChangeCurrentStageAsync(GameStages stage, int time);

    public void ChangeOfficeState(int officeId, OfficeStates officeState);
}