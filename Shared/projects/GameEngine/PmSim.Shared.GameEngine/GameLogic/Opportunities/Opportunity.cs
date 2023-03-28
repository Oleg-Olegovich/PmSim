using PmSim.Shared.GameEngine.Interfaces;

namespace PmSim.Shared.GameEngine.GameLogic.Opportunities
{
    internal abstract class Opportunity : ITranslationObject, IPlayerAction, IAuctionLot
    {
        public abstract int DescriptionNumber { get; }
        
        internal abstract void Process(Player targetPlayer);
    }
}