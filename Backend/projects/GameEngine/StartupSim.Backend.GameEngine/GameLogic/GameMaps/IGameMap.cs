using StartupSim.Backend.GameEngine.Dto;

namespace StartupSim.Backend.GameEngine.GameLogic.GameMaps
{

        public interface IGameMap
        {
                int MapImageNumber { get; }

                Office[] Offices { get; }
        }
}