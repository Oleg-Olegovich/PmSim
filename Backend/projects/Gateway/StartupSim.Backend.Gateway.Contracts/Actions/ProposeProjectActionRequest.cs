namespace StartupSim.Backend.Gateway.Contracts.Actions
{
    public class ProposeProjectActionRequest : ProjectAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}