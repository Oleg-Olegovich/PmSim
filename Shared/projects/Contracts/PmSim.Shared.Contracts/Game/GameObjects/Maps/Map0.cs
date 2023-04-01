using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.GameObjects.Maps;

public class Map0 : IGameMap
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