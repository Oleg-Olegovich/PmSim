using System.Linq;

namespace PmSim.Backend.GameEngine.GameLogic.Incidents
{

    public class TurningInternet : Incident
    {
        public override int DescriptionNumber => 0;
        public override int PassCost => 10;

        public override void Process(Player targetPlayer)
        {
            foreach (var project in targetPlayer.Projects.Where(project => project.IsDone))
            {
                targetPlayer.Money -= project.Reward.Revenue;
            }
        }
    }
}