using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Shared.Contracts.Game;

public static class GameConstants
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
    public static int DefaultManagementRealTime => 300;

    /// <summary>
    /// Sets how much maximum time is given to the players for an action.
    /// Default settings.
    /// </summary>
    public static int DefaultIncidentRealTime => 60;

    public static int DefaultSprintActionsNumbers => 2;

    public static int DefaultAuctionRealTime => 10;

    public static int DefaultStartUpCapital => 10;
    
    public static int DefaultMaxSprintNumber => 10;

    public static int MaxActorsNumber => 10;

    public static int MaxSkillLevel => 10;

    public static int TechSupportSalary => 1;

    public static int MaleNamesNumber => 5;

    public static int MaleSurnamesNumber => 5;

    public static int FemaleNamesNumber => 5;

    public static int FemaleSurnamesNumber => 5;

    public static Project[] Projects { get; }

    public static Feature[] Features { get; }

    static GameConstants()
    {
        Features = new[]
        {
            new Feature(0, new ProgressPoints(3, 1, 1, 2), new Reward(0, 1, 0)),
            new Feature(1, new ProgressPoints(3, 0, 1, 2), new Reward(0, 1, 0)),
            new Feature(2, new ProgressPoints(5, 1, 2, 3), new Reward(0, 1, 1)),
        };
        Projects = new[]
        {
            new Project(0, new ProgressPoints(10, 9, 0, 6), new Reward(0, 40, 1), 4, new[]
            {
                new Feature(Features[0])
            }),
            new Project(1, new ProgressPoints(6, 3, 0, 4), new Reward(30, 0, 0), 3, new[]
            {
                new Feature(Features[1])
            }),
            new Project(2, new ProgressPoints(13, 11, 0, 10), new Reward(0, 40, 1), 4, new[]
            {
                new Feature(Features[2])
            }),
            new Project(3, new ProgressPoints(15, 7, 0, 6), new Reward(0, 30, 0), 4, null),
            new Project(4, new ProgressPoints(10, 6, 0, 4), new Reward(0, 20, 0), 3, null),
            new Project(5, new ProgressPoints(11, 4, 0, 6), new Reward(0, 15, 1), 2, null),
            new Project(6, new ProgressPoints(9, 6, 0, 4), new Reward(0, 25, 1), 2, null),
            new Project(7, new ProgressPoints(6, 3, 0, 3), new Reward(30, 0, 1), 2, null),
            new Project(8, new ProgressPoints(4, 3, 10, 3), new Reward(30, 0, 0), 2, null),
            new Project(9, new ProgressPoints(4, 3, 0, 3), new Reward(30, 0, 0), 2, null),
            new Project(10, new ProgressPoints(20, 22, 10, 8), new Reward(100, 0, 0), 5, null),
            new Project(11, new ProgressPoints(20, 22, 10, 8), new Reward(100, 0, 0), 5, null),
            new Project(12, new ProgressPoints(15, 13, 10, 7), new Reward(70, 0, 0), 4, null),
            new Project(13, new ProgressPoints(5, 7, 0, 3), new Reward(0, 15, 0), 2, null),
            new Project(14, new ProgressPoints(20, 10, 0, 6), new Reward(50, 0, 0), 4, null),
            new Project(15, new ProgressPoints(15, 7, 0, 3), new Reward(34, 0, 0), 3, null),
            new Project(16, new ProgressPoints(10, 3, 0, 3), new Reward(20, 0, 0), 2, null),
            new Project(17, new ProgressPoints(10, 6, 0, 6), new Reward(20, 0, 0), 3, null),
            new Project(18, new ProgressPoints(100, 0, 0, 15), new Reward(200, 50, 5), 8, null),
        };
    }

    public static SkillsPoints GetSkillsByProfession(Professions profession)
        => profession switch
        {
            Professions.Programmer => new SkillsPoints(3, 1, 1, 1, 1),
            Professions.Musician => new SkillsPoints(1, 3, 1, 1, 1),
            Professions.Designer => new SkillsPoints(1, 1, 3, 1, 1),
            Professions.Manager => new SkillsPoints(1, 1, 1, 3, 1),
            _ => new SkillsPoints(1, 1, 1, 1, 1)
        };
}