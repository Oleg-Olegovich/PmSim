namespace StartupSim.Backend.Gateway.Contracts.Actions
{
    public class ProjectAuctionActionRequest : ActionRequest
    {
        public int ProjectNumber { get; set; }
        
        public int StartPrice { get; set; }
    }
}