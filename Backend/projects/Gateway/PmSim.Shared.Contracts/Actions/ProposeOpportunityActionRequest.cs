namespace PmSim.Shared.Contracts.Actions
{
    public class ProposeOpportunityActionRequest : OpportunityAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}