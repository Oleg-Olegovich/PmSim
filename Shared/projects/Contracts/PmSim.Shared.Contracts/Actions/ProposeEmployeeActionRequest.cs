namespace PmSim.Shared.Contracts.Actions
{
    public class ProposeEmployeeActionRequest : EmployeeAuctionActionRequest
    {
        public int BuyerId { get; set; }
    }
}