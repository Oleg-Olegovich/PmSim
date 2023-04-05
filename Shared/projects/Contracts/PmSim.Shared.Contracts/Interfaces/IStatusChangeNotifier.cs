﻿using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Others;

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
    
    public int ProjectsNumber { set; }
    
    public int EmployeesNumber { set; }
    
    public int Programming { set; }
    
    public int Music { set; }
    
    public int Design { set; }
    
    public int Management { set; }
    
    public int Creativity { set; }

    public PlayerStatus AnotherPlayerStatus { set; }
    
    public IEnumerable<PlayerStatus> Players { set; }

    public Task ChangeCurrentStageAsync(GameStages stage, int time);
}