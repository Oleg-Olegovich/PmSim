using System;
using PmSim.Shared.GameEngine.GameLogic.Opportunities;

namespace PmSim.Shared.GameEngine.Dto
{
    /// <summary>
    /// Data transfer object.
    /// </summary>
    internal class Reward : ICloneable
    {
        internal int Prize { get; }

        internal int Revenue { get; }

        /// <summary>
        /// The reward brings several random opportunities.
        /// </summary>
        internal int Opportunities { get; }

        internal bool IsCollected { get; set; }

        internal Reward(int prize, int revenue, int opportunities)
        {
            Prize = prize;
            Revenue = revenue;
            Opportunities = opportunities;
        }

        public object Clone()
            => MemberwiseClone();
    }
}