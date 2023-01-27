using StartupSim.Backend.GameEngine.GameLogic.Opportunities;

namespace StartupSim.Backend.GameEngine.GameLogic.Incidents
{
    public abstract class Incident : Opportunity
    {
        public abstract int PassCost { get; }

        public int DonationsSum { get; set; }
    }
}