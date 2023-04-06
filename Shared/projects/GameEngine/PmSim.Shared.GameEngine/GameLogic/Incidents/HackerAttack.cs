using System;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;

namespace PmSim.Shared.GameEngine.GameLogic.Incidents
{
    internal class HackerAttack : Incident
    {
        public override int DescriptionNumber => 2;

        internal override int PassCost => 10;
        
        internal override void Process(Player targetPlayer)
        {
            var index = new Random().Next(targetPlayer.Projects.Count);
            targetPlayer.Projects[index].ResetToBackUp();
        }
    }
}