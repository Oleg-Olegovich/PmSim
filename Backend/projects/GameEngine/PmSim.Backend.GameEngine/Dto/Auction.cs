using PmSim.Backend.GameEngine.Exceptions;
using PmSim.Backend.GameEngine.Interfaces;

namespace PmSim.Backend.GameEngine.Dto
{
    public class Auction
    {
        public int Id { get; }

        public IAuctionLot Lot { get; }

        public int SellerId { get; }

        public int StartPrice { get; }

        public int LastBuyerId { get; set; }

        public int LastPrice { get; set; }

        public bool IsPublic { get; }

        /// <summary>
        /// For personal.
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// If buyerId equals to -1, then the auction is public.
        /// </summary>
        public Auction(int id, IAuctionLot lot, int sellerId, int startPrice, int buyerId = -1)
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