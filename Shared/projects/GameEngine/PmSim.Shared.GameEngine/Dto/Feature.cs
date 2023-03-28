using System;
using PmSim.Shared.GameEngine.Interfaces;

namespace PmSim.Shared.GameEngine.Dto
{
    internal class Feature : ITranslationObject, ICloneable
    {
        public int DescriptionNumber { get; }

        internal ProgressPoints Points { get; }

        internal Reward Reward { get; }
        
        internal bool IsDone
            => Points.IsDone;

        internal Feature(int nameNumber, ProgressPoints points, Reward reward)
        {
            DescriptionNumber = nameNumber;
            Points = points;
            Reward = reward;
        }

        public virtual object Clone()
            => new Feature(DescriptionNumber, Points.Clone() as ProgressPoints, Reward.Clone() as Reward);
    }
}