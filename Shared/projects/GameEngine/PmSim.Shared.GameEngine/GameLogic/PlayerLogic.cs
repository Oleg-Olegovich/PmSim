using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.GameObjects.Others;

namespace PmSim.Shared.GameEngine.GameLogic;

internal static class PlayerLogic
{
    internal static PlayerStatus GetStatus(Player player)
        => new()
        {
            Id = player.Id,
            Name = player.Name,
            Money = player.Money,
            CompletedProjects = player.CompletedProjects
        };
    
    internal static void SetSkillsByProfession(Player player, Professions profession)
    {
        var skills = GameConstants.GetSkillsByProfession(profession);
        player.StatusChangeNotifier.Programming = player.Skills.Programming = skills.Programming;
        player.StatusChangeNotifier.Music = player.Skills.Music = skills.Music;
        player.StatusChangeNotifier.Design = player.Skills.Design = skills.Design;
        player.StatusChangeNotifier.Management = player.Skills.Management = skills.Management;
        player.StatusChangeNotifier.Creativity = player.Skills.Creativity = skills.Creativity;
        if (profession == Professions.Major)
        {
            player.Money *= 2;
        }
    }
    
    /// <summary>
    /// Sums up the results of the sprint.
    /// </summary>
    internal static void SummingUp(Player player)
    {
        player.ActionsNumber = 0;
        SummarizeProjectsResults(player);
        CollectIncome(player);
        CheckIsOut(player);
    }

    /// <summary>
    /// Summarizes the results of the developments.
    /// If a project or feature has been completed, they begin to make a income.
    /// The reward is awarded immediately.
    /// </summary>
    private static void SummarizeProjectsResults(Player player)
    {
        var random = new Random();
        foreach (var project in player.Projects)
        {
            if (project.IsDone && !project.Reward.IsCollected)
            {
                player.Money += project.Reward.Prize;
                for (var i = 0; i < project.Reward.Opportunity; ++i)
                {
                    player.Opportunities.Add(random.Next(1));
                }

                project.Reward.IsCollected = true;
                continue;
            }

            project.Deadline -= 1;
        }
    }

    /// <summary>
    /// Collects income from projects.
    /// </summary>
    private static void CollectIncome(Player player)
    {
        foreach (var project in player.Projects.Where(project => project.IsDone))
        {
            player.Money += project.Reward.Revenue;
        }
    }
    
    private static void CheckIsOut(Player player)
    {
        if (player.Money < 0)
        {
            player.IsOut = true;
        }
    }
}