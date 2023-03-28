using PmSim.Shared.GameEngine.Dto;

namespace PmSim.Shared.GameEngine.GameLogic.GameMaps;

internal interface IGameMap
{
        public int MapImageNumber { get; }

        public Office[] Offices { get; }
}