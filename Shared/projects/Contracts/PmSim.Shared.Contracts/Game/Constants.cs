using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.Contracts.Game;

public static class Constants
{
    public static int SmallOfficeCapacity => 5;
    
    public static int MiddleOfficeCapacity => 10;
    
    public static int BigOfficeCapacity => 15;
    
    public static int SmallOfficeRentPrice => 1;
    
    public static int MiddleOfficeRentPrice => 2;
    
    public static int BigOfficeRentPrice => 3;
    
    public static int WorkForHireSalary => 1;
    
    public static int MaxSalary => 10;

    public static int DefaultMaxPlayersNumber => 2;
    
    public static int DefaultMaxBotsNumber => 3;
    
    public static GameModes DefaultMode => GameModes.Survival;
    
    public static int DefaultMapNumber => 0;
    
    /// <summary>
    /// Sets how much maximum time is given to the players for an action.
    /// Default settings.
    /// </summary>
    public static int DefaultConnectionRealTime => 60;

    /// <summary>
    /// Sets how much maximum time is given to the players for an action.
    /// Default settings.
    /// </summary>
    public static int DefaultChoosingBackground => 60;

    /// <summary>
    /// Sets how much maximum time is given to the players for an action.
    /// Default settings.
    /// </summary>
    public static int DefaultSprintRealTime => 180;

    /// <summary>
    /// Sets how much maximum time is given to the players for an action.
    /// Default settings.
    /// </summary>
    public static int DefaultDiplomacyRealTime => 180;

    /// <summary>
    /// Sets how much maximum time is given to the players for an action.
    /// Default settings.
    /// </summary>
    public static int DefaultIncidentRealTime => 60;

    public static int DefaultSprintActionsNumbers => 2;

    public static int DefaultAuctionRealTime => 10;

    public static int DefaultStartUpCapital => 10;

    public static int MaxActorsNumber => 10;

    public static int MaxSkillLevel => 10;

    public static int TechSupportSalary => 1;

    public static int MaleNamesNumber => 5;

    public static int MaleSurnamesNumber => 5;

    public static int FemaleNamesNumber => 5;

    public static int FemaleSurnamesNumber => 5;
}