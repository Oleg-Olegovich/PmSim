using PmSim.Backend.GameEngine.Dto;

namespace PmSim.Backend.GameEngine.GameLogic.GameMaps
{

        public interface IGameMap
        {
                int MapImageNumber { get; }

                Office[] Offices { get; }
        }
}