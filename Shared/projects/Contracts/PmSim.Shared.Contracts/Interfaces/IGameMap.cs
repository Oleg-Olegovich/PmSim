using PmSim.Shared.Contracts.Game.GameObjects.Others;

namespace PmSim.Shared.Contracts.Interfaces;

public interface IGameMap
{
        public int MapImageNumber { get; }

        public Office[] Offices { get; }
}