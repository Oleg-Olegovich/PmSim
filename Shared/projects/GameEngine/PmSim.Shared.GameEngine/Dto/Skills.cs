using PmSim.Shared.Contracts.Game;

namespace PmSim.Shared.GameEngine.Dto
{
    internal class SkillsPoints
    {
        private int _programming;
        private int _music;
        private int _design;
        private int _management;
        private int _creativity;
        
        internal int Programming
        {
            get => _programming;
            set => SetSkill(ref _programming, value);
        }
        
        internal int Music
        {
            get => _music;
            set => SetSkill(ref _music, value);
        }
        
        internal int Design
        {
            get => _design;
            set => SetSkill(ref _design, value);
        }
        
        internal int Management
        {
            get => _management;
            set => SetSkill(ref _management, value);
        }
        
        internal int Creativity
        {
            get => _creativity;
            set => SetSkill(ref _creativity, value);
        }

        internal SkillsPoints(int programming, int music, int design, int management, int creativity)
        {
            _programming = programming;
            _music = music;
            _design = design;
            _management = management;
            _creativity = creativity;
        }

        private static void SetSkill(ref int skill, int value)
        {
            if (value >= 0 && value < Constants.MaxSkillLevel)
            {
                skill = value;
            }
        }
    }
}