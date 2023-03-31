using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.GameObjects.Projects
{
    public class Feature : ITranslationObject, ICloneable
    {
        public int DescriptionNumber { get; }

        public ProgressPoints Points { get; }

        public Reward Reward { get; }
        
        public bool IsDone
            => Points.IsDone;

        public Feature(int nameNumber, ProgressPoints points, Reward reward)
        {
            DescriptionNumber = nameNumber;
            Points = points;
            Reward = reward;
        }

        public virtual object Clone()
            => new Feature(DescriptionNumber, Points.Clone() as ProgressPoints, Reward.Clone() as Reward);
    }
}