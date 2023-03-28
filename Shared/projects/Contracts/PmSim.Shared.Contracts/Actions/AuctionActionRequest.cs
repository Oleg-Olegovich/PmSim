namespace PmSim.Shared.Contracts.Actions
{
    public class AuctionActionRequest : ActionRequest
    {
        public int AuctionNumber { get; set; }
        
        public int OfferSum { get; set; }
    }
}