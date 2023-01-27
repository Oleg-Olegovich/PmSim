namespace StartupSim.Backend.Gateway.Contracts.Actions
{
    public class ProposeExecutorActionRequest : ExecutorAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}