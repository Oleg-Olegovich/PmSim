using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.GameEngine.GameLogic.Incidents
{
    internal class PowerFailure : Incident
    {
        public override int DescriptionNumber => 1;

        internal override int PassCost => 15;
        
        internal override void Process(Player targetPlayer)
        {
            foreach (var project in targetPlayer.Projects)
            {
                project.ResetToBackUp();
            }
        }
    }
}