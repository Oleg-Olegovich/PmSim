using System.Collections.ObjectModel;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Others;

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
    
    public int ProjectsNumber { get; set; }
    
    public int EmployeesNumber { get; set; }
    
    public ObservableCollection<PlayerStatus> Players { get; }
}