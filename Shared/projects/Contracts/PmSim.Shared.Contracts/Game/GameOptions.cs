using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.Contracts.Game;

public class GameOptions
{
    public static GameOptions Default
        => new("Default", Constants.DefaultMaxPlayersNumber, Constants.DefaultMaxBotsNumber, 
            Constants.DefaultMode, Constants.DefaultMapNumber, Constants.DefaultConnectionRealTime, 
            Constants.DefaultChoosingBackground, Constants.DefaultSprintRealTime, 
            Constants.DefaultDiplomacyRealTime, Constants.DefaultIncidentRealTime, 
            Constants.DefaultSprintActionsNumbers, Constants.DefaultAuctionRealTime, Constants.DefaultStartUpCapital);
    
    public string GameName { get; set; }
    
    public int MaxPlayersNumber { get; set; }
    
    public int BotsNumber { get; set; }
    
    public GameModes Mode { get; set; }
    
    public int MapNumber { get; set; }
    
    public int ConnectionRealTime { get; set; }

    public int ChoosingBackgroundRealTime { get; set; }

    public int SprintRealTime { get; set; }

    public int DiplomacyRealTime { get; set; }

    public int IncidentRealTime { get; set; }

    public int SprintActionsNumbers { get; set; }

    public int AuctionRealTime { get; set; }

    public int StartUpCapital { get; set; }

    public GameOptions(string gameName, int maxPlayersNumber, int botsNumber, GameModes mode, int mapNumber, 
        int connectionRealTime, int choosingBackgroundRealTime, int sprintRealTime, int diplomacyRealTime, 
        int incidentRealTime, int sprintActionsNumbers, int auctionRealTime, int startUpCapital)
    {
        GameName = gameName;
        MaxPlayersNumber = maxPlayersNumber;
        BotsNumber = botsNumber;
        Mode = mode;
        MapNumber = mapNumber;
        ConnectionRealTime = connectionRealTime;
        ChoosingBackgroundRealTime = choosingBackgroundRealTime;
        SprintRealTime = sprintRealTime;
        DiplomacyRealTime = diplomacyRealTime;
        IncidentRealTime = incidentRealTime;
        SprintActionsNumbers = sprintActionsNumbers;
        AuctionRealTime = auctionRealTime;
        StartUpCapital = startUpCapital;
    }
}