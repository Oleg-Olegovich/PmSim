using System;
using PmSim.Backend.GameEngine.Dto;

namespace PmSim.Backend.GameEngine.GameLogic.Incidents
{
    public class HackerAttack : Incident
    {
        public override int DescriptionNumber => 2;

        public override int PassCost => 10;
        
        public override void Process(Player targetPlayer)
        {
            var index = new Random().Next(targetPlayer.Projects.Count);
            targetPlayer.Projects[index] = targetPlayer.Projects[index].LastBackUp.Clone() as Project;
        }
    }
}