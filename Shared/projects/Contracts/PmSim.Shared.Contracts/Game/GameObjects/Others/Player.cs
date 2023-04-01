using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others;

public class Player : Employee
{
    public IStatusChangeNotifier StatusChangeNotifier { get; set; }

    private int _money;
    public int Money
    {
        get => _money;
        set => StatusChangeNotifier.Money = _money = value;
    }
    
    private int _completedProjects;
    public int CompletedProjects
    {
        get => _completedProjects;
        set => StatusChangeNotifier.ProjectsNumber = _completedProjects = value;
    }

    private int _actionsNumber;
    /// <summary>
    /// This is necessary so that the player cannot exceed the number of action per stage.
    /// </summary>
    public int ActionsNumber
    {
        get => _actionsNumber;
        set => StatusChangeNotifier.ActionsNumber = _actionsNumber = value;
    }
    
    public bool IsStartupOpen { get; set; }

    public bool IsOut { get; set; }

    public string Name { get; }

    public int Id { get; }

    /// <summary>
    /// This repository contains projects.
    /// </summary>
    public List<Project> Projects { get; } = new();

    public List<int> Opportunities { get; } = new();
        
    public bool IsBackgroundChosen { get; set; }

    public Player(int id, string name, int capital, IStatusChangeNotifier notifier)
    {
        StatusChangeNotifier = notifier;
        Id = id;
        Name = name;
        Money = capital;
    }

    protected Player(int id, string name, Professions profession, int capital, IStatusChangeNotifier notifier)
        : base(GameConstants.GetSkillsByProfession(profession))
    {
        StatusChangeNotifier = notifier;
        Id = id;
        Money = capital;
        if (profession == Professions.Major)
        {
            Money *= 2;
        }

        Name = name;
    }
}