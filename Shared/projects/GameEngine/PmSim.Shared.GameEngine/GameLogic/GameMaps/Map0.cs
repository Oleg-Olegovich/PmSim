using PmSim.Shared.GameEngine.Dto;
using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.GameEngine.GameLogic.GameMaps
{
    internal class Map0 : IGameMap
    {
        public int MapImageNumber => 0;

        public Office[] Offices { get; }
            = {
                new(OfficeTypes.Small),
                new(OfficeTypes.Small),
                new(OfficeTypes.Small),
                new(OfficeTypes.Small),
                new(OfficeTypes.Small),
                new(OfficeTypes.Middle),
                new(OfficeTypes.Middle),
                new(OfficeTypes.Middle),
                new(OfficeTypes.Big),
                new(OfficeTypes.Big),
            };
    }
}