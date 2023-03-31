namespace PmSim.Shared.Contracts.Game.GameObjects.Projects
{
    /// <summary>
    /// Data transfer object.
    /// </summary>
    public class Reward : ICloneable
    {
        public int Prize { get; }

        public int Revenue { get; }

        /// <summary>
        /// The reward brings several random opportunities.
        /// </summary>
        public int Opportunities { get; }

        public bool IsCollected { get; set; }

        public Reward(int prize, int revenue, int opportunities)
        {
            Prize = prize;
            Revenue = revenue;
            Opportunities = opportunities;
        }

        public object Clone()
            => MemberwiseClone();
    }
}