using PmSim.Backend.GameEngine.GameLogic.Opportunities;

namespace PmSim.Backend.GameEngine.GameLogic.Incidents
{
    public abstract class Incident : Opportunity
    {
        public abstract int PassCost { get; }

        public int DonationsSum { get; set; }
    }
}