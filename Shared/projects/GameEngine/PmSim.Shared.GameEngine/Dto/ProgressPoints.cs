using System;

namespace PmSim.Shared.GameEngine.Dto
{
    internal class ProgressPoints : ICloneable
    {
        private int _programming = 0, _design = 0, _music = 0, _management = 0;
        
        internal int Programming
        {
            get => _programming;
            set => _programming = value < 0 ? 0 : value;
        }
        
        internal int Design
        {
            get => _design;
            set => _design = value < 0 ? 0 : value;
        }
        
        internal int Music
        {
            get => _music;
            set => _music = value < 0 ? 0 : value;
        }

        internal int Management
        {
            get => _management;
            set => _management = value < 0 ? 0 : value;
        }
        
        internal bool IsDone
            => Programming == 0 && Music == 0 && Design == 0 && Management == 0;

        internal ProgressPoints(int programming, int design, int music, int management)
        {
            Programming = programming;
            Music = music;
            Design = design;
            Management = management;
        }

        public object Clone()
            => MemberwiseClone();
    }
}