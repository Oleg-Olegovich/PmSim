namespace PmSim.Shared.Contracts.Game.Projects;

/// <summary>
/// Data transfer object.
/// </summary>
public class Reward
{
    public int Prize { get; }

    public int Revenue { get; }

    /// <summary>
    /// The reward brings several random opportunities.
    /// </summary>
    public int Opportunity { get; }

    public bool IsCollected { get; set; }

    public Reward(int prize, int revenue, int opportunity)
    {
        Prize = prize;
        Revenue = revenue;
        Opportunity = opportunity;
    }

    public Reward(Reward reward)
        : this(reward.Prize, reward.Revenue, reward.Opportunity)
    { }
}