using PmSim.Shared.GameEngine.Exceptions;
using PmSim.Shared.GameEngine.Interfaces;

namespace PmSim.Shared.GameEngine.Dto
{
    internal class Auction
    {
        internal int Id { get; }

        internal IAuctionLot Lot { get; }

        internal int SellerId { get; }

        internal int StartPrice { get; }

        internal int LastBuyerId { get; set; }

        internal int LastPrice { get; set; }

        internal bool IsPublic { get; }

        /// <summary>
        /// For personal.
        /// </summary>
        internal bool Accepted { get; set; }

        /// <summary>
        /// If buyerId equals to -1, then the auction is internal.
        /// </summary>
        internal Auction(int id, IAuctionLot lot, int sellerId, int startPrice, int buyerId = -1)
        {
            if (sellerId == buyerId || buyerId == -1 && startPrice < 0 || buyerId != -1 && startPrice < 1
                || lot == null)
            {
                throw new GameLogicException("Attempt to create an incorrect auction.");
            }

            Id = id;
            Lot = lot;
            SellerId = sellerId;
            StartPrice = startPrice;
            if (buyerId == -1)
            {
                IsPublic = true;
            }

            LastBuyerId = buyerId;
        }
    }
}