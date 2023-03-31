using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others;

public class Player : Employee
{
    public int Money { get; set; }

    public int CompletedProjects { get; set; }

    public bool IsStartupOpen { get; set; }

    public bool IsOut { get; set; }

    public string Name { get; }

    public int Id { get; }

    /// <summary>
    /// This is necessary so that the player cannot exceed the number of action per stage.
    /// </summary>
    public int ActionsNumber { get; set; }

    /// <summary>
    /// This repository contains projects.
    /// </summary>
    public List<Project> Projects { get; } = new();

    public List<int> Opportunities { get; } = new();
        
    public bool IsBackgroundChosen { get; set; }

    public Player(int id, string name, int capital)
    {
        Id = id;
        Name = name;
        Money = capital;
    }

    public Player(int id, string name, Professions profession, int capital)
        : base(GameConstants.GetSkillsByProfession(profession))
    {
        Id = id;
        Money = capital;
        if (profession == Professions.Major)
        {
            Money *= 2;
        }

        Name = name;
    }
}