using PmSim.Shared.GameEngine.GameLogic.Opportunities;

namespace PmSim.Shared.GameEngine.GameLogic.Incidents
{
    internal abstract class Incident : Opportunity
    {
        internal abstract int PassCost { get; }

        internal int DonationsSum { get; set; }
    }
}