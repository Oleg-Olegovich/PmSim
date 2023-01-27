using StartupSim.Backend.GameEngine.Dto;

namespace StartupSim.Backend.GameEngine.GameLogic.Incidents
{
    public class PowerFailure : Incident
    {
        public override int DescriptionNumber => 1;

        public override int PassCost => 15;
        
        public override void Process(Player targetPlayer)
        {
            for (var i = 0; i < targetPlayer.Projects.Count; ++i)
            {
                targetPlayer.Projects[i] = targetPlayer.Projects[i].LastBackUp.Clone() as Project;
            }
        }
    }
}