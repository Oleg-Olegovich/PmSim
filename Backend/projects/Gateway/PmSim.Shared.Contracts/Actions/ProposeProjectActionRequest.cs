namespace PmSim.Shared.Contracts.Actions
{
    public class ProposeProjectActionRequest : ProjectAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}