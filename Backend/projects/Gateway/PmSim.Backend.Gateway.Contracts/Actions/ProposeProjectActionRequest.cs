namespace PmSim.Backend.Gateway.Contracts.Actions
{
    public class ProposeProjectActionRequest : ProjectAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}