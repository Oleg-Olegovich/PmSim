namespace PmSim.Backend.Gateway.Contracts.Actions
{
    public class ProposeOpportunityActionRequest : OpportunityAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}