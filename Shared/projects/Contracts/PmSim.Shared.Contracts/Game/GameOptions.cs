using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.Contracts.Game;

public class GameOptions
{
    public static GameOptions Default
        => new("Default", GameConstants.DefaultMaxPlayersNumber, GameConstants.DefaultMaxBotsNumber, 
            GameConstants.DefaultMode, GameConstants.DefaultMapNumber, GameConstants.DefaultConnectionRealTime, 
            GameConstants.DefaultChoosingBackground, GameConstants.DefaultManagementRealTime, GameConstants.DefaultIncidentRealTime, 
            GameConstants.DefaultSprintActionsNumbers, GameConstants.DefaultAuctionRealTime, 
            GameConstants.DefaultStartUpCapital, GameConstants.DefaultMaxSprintNumber);
    
    public string GameName { get; set; }
    
    public int MaxPlayersNumber { get; set; }
    
    public int BotsNumber { get; set; }
    
    public GameModes Mode { get; set; }
    
    public int MapNumber { get; set; }
    
    public int ConnectionRealTime { get; set; }

    public int ChoosingBackgroundRealTime { get; set; }

    public int SprintRealTime { get; set; }

    public int IncidentRealTime { get; set; }

    public int SprintActionsNumbers { get; set; }

    public int AuctionRealTime { get; set; }

    public int StartUpCapital { get; set; }
    
    public int MaxSprintNumber { get; set; }

    public GameOptions(string gameName, int maxPlayersNumber, int botsNumber, GameModes mode, int mapNumber, 
        int connectionRealTime, int choosingBackgroundRealTime, int sprintRealTime, int incidentRealTime, 
        int sprintActionsNumbers, int auctionRealTime, int startUpCapital, int mapSprintNumber)
    {
        GameName = gameName;
        MaxPlayersNumber = maxPlayersNumber;
        BotsNumber = botsNumber;
        Mode = mode;
        MapNumber = mapNumber;
        ConnectionRealTime = connectionRealTime;
        ChoosingBackgroundRealTime = choosingBackgroundRealTime;
        SprintRealTime = sprintRealTime;
        IncidentRealTime = incidentRealTime;
        SprintActionsNumbers = sprintActionsNumbers;
        AuctionRealTime = auctionRealTime;
        StartUpCapital = startUpCapital;
        MaxSprintNumber = mapSprintNumber;
    }
}