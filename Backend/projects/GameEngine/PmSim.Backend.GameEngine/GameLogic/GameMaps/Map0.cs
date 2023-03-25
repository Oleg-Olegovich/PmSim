using PmSim.Backend.GameEngine.Dto;
using PmSim.Shared.Contracts.Enums;

namespace PmSim.Backend.GameEngine.GameLogic.GameMaps
{
    public class Map0 : IGameMap
    {
        public int MapImageNumber => 0;

        public Office[] Offices { get; }
            = {
                new Office(OfficeTypes.Small),
                new Office(OfficeTypes.Small),
                new Office(OfficeTypes.Small),
                new Office(OfficeTypes.Small),
                new Office(OfficeTypes.Small),
                new Office(OfficeTypes.Middle),
                new Office(OfficeTypes.Middle),
                new Office(OfficeTypes.Middle),
                new Office(OfficeTypes.Big),
                new Office(OfficeTypes.Big),
            };
    }
}