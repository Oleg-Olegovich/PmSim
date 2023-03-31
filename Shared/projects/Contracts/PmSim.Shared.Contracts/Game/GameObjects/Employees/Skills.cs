using PmSim.Shared.Contracts.Game;

namespace PmSim.Shared.GameEngine.Dto
{
    public class SkillsPoints
    {
        private int _programming;
        private int _music;
        private int _design;
        private int _management;
        private int _creativity;
        
        public int Programming
        {
            get => _programming;
            set => SetSkill(ref _programming, value);
        }
        
        public int Music
        {
            get => _music;
            set => SetSkill(ref _music, value);
        }
        
        public int Design
        {
            get => _design;
            set => SetSkill(ref _design, value);
        }
        
        public int Management
        {
            get => _management;
            set => SetSkill(ref _management, value);
        }
        
        public int Creativity
        {
            get => _creativity;
            set => SetSkill(ref _creativity, value);
        }

        public SkillsPoints(int programming, int music, int design, int management, int creativity)
        {
            _programming = programming;
            _music = music;
            _design = design;
            _management = management;
            _creativity = creativity;
        }

        private static void SetSkill(ref int skill, int value)
        {
            if (value >= 0 && value < GameConstants.MaxSkillLevel)
            {
                skill = value;
            }
        }
    }
}