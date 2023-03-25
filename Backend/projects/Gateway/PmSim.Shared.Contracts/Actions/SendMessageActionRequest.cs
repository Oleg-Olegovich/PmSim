namespace PmSim.Shared.Contracts.Actions
{
    public class SendMessageActionRequest : SendMessageToEveryoneActionRequest
    {
        public int RecipientId { get; set; }
    }
}