using PmSim.Backend.GameEngine.Interfaces;

namespace PmSim.Backend.GameEngine.GameLogic.Opportunities
{
    public abstract class Opportunity : ITranslationObject, IPlayerAction, IAuctionLot
    {
        public abstract int DescriptionNumber { get; }
        
        public abstract void Process(Player targetPlayer);
    }
}