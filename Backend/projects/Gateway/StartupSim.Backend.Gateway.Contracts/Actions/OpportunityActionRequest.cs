namespace StartupSim.Backend.Gateway.Contracts.Actions
{
    public class OpportunityActionRequest : ActionRequest
    {
        public int OpportunityNumber { get; set; }

        public int TargetPlayer { get; set; }
    }
}