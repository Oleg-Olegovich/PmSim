namespace PmSim.Backend.Gateway.Contracts.Actions
{
    public class SendMessageActionRequest : SendMessageToEveryoneActionRequest
    {
        public int RecipientId { get; set; }
    }
}