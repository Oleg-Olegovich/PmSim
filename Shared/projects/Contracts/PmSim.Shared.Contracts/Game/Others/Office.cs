using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.Contracts.Game.Others;

public class Office
{
    public int OwnerId { get; set; } = -1;

    public int Capacity { get; }
        
    public int RentalPrice { get; }

    public Office(OfficeTypes type)
    {
        switch (type)
        {
            case OfficeTypes.Small:
                Capacity = GameConstants.SmallOfficeCapacity;
                RentalPrice = GameConstants.SmallOfficeRentPrice;
                break;
            case OfficeTypes.Middle:
                Capacity = GameConstants.MiddleOfficeCapacity;
                RentalPrice = GameConstants.MiddleOfficeRentPrice;
                break;
            case OfficeTypes.Big:
            default:
                Capacity = GameConstants.BigOfficeCapacity;
                RentalPrice = GameConstants.BigOfficeRentPrice;
                break;
        }
    }
}