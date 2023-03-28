using System.Linq;

namespace PmSim.Shared.GameEngine.GameLogic.Incidents
{

    internal class TurningInternet : Incident
    {
        public override int DescriptionNumber => 0;
        internal override int PassCost => 10;

        internal override void Process(Player targetPlayer)
        {
            foreach (var project in targetPlayer.Projects.Where(project => project.IsDone))
            {
                targetPlayer.Money -= project.Reward.Revenue;
            }
        }
    }
}