using System;
using StartupSim.Backend.GameEngine.Interfaces;

namespace StartupSim.Backend.GameEngine.Dto
{
    public class Project : Feature, IAuctionLot
    {
        public Feature[] Features { get; private set; }

        private int _deadline;
        public int Deadline
        {
            get => _deadline;
            set => _deadline = value < 0 ?  0 : value;
        }

        public bool IsFailed
            => Deadline == 0 && !IsDone;

        public Project LastBackUp { get; set; }

        public Project(int nameNumber, ProgressPoints points, Reward reward, int deadline, Feature[] features, bool isBackUp = true)
            : base(nameNumber, points, reward)
        {
            _deadline = deadline;
            Features = features == null ? features : Array.Empty<Feature>();
            if (isBackUp)
            {
                LastBackUp = MakeClone();
            }
        }

        public override object Clone()
            => MakeClone();

        private Project MakeClone()
        {
            if (Features == null)
            {
                Features = Array.Empty<Feature>();
            }
            var features = new Feature[Features.Length];
            for (var i = 0; i < features.Length; ++i)
            {
                features[i] = Features[i].Clone() as Feature;
            }

            return new Project(DescriptionNumber, Points.Clone() as ProgressPoints, Reward.Clone() as Reward, Deadline,
                features, false);
        }
    }
}