using StartupSim.Backend.GameEngine.Interfaces;

namespace StartupSim.Backend.GameEngine.GameLogic.Opportunities
{
    public abstract class Opportunity : ITranslationObject, IPlayerAction, IAuctionLot
    {
        public abstract int DescriptionNumber { get; }
        
        public abstract void Process(Player targetPlayer);
    }
}