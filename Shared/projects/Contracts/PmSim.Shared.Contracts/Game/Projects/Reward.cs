namespace PmSim.Shared.Contracts.Game.Projects;

/// <summary>
/// Data transfer object.
/// </summary>
public class Reward
{
    public int Prize { get; set; }

    public int Revenue { get; }

    /// <summary>
    /// The reward brings several random opportunities.
    /// </summary>
    public int Opportunities { get; set; }

    public Reward(int prize, int revenue, int opportunities)
    {
        Prize = prize;
        Revenue = revenue;
        Opportunities = opportunities;
    }

    public Reward(Reward reward)
        : this(reward.Prize, reward.Revenue, reward.Opportunities)
    { }
}