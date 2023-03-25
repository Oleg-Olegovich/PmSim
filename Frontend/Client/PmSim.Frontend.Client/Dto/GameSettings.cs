namespace PmSim.Frontend.Client.Dto;

public class GameSettings
{
    public string GameName { get; set; }
    
    public int MaxPlayersNumber { get; set; }
    
    public int BotsNumber { get; set; }
    
    public int Mode { get; set; }
    
    public int MapNumber { get; set; }
    
    public int ConnectionRealTime { get; set; }

    public int ChoosingBackgroundRealTime { get; set; }

    public int SprintRealTime { get; set; }

    public int DiplomacyRealTime { get; set; }

    public int IncidentRealTime { get; set; }

    public int SprintActionsNumbers { get; set; }

    public int AuctionRealTime { get; set; }

    public int StartUpCapital { get; set; }

    public static GameSettings Default
        => new GameSettings("Default", 1, 4, 0, 0, 
            60, 60, 180, 180, 
            60, 10, 2, 10);

    public GameSettings(string gameName, int maxPlayersNumber, int botsNumber, int mode, int mapNumber, 
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