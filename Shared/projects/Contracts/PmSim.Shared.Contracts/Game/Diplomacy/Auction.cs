using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.Diplomacy;

public class Auction
{
    public int Id { get; }

    public IAuctionLot Lot { get; }

    public int SellerId { get; }
    
    public int LastBuyerId { get; set; }

    public int LastPrice { get; set; }

    public Auction(int id, IAuctionLot lot, int sellerId, int startPrice)
    {
        if (startPrice < 1 || lot == null)
        {
            throw new PmSimException("Attempt to create an incorrect auction.");
        }

        Id = id;
        Lot = lot;
        SellerId = sellerId;
        LastPrice = startPrice;
        LastBuyerId = -1;
    }
}