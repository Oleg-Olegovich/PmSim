namespace PmSim.Shared.Contracts.Actions
{
    public class ProposeExecutorActionRequest : ExecutorAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}