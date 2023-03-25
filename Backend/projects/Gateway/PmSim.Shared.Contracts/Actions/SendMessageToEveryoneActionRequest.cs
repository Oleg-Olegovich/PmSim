namespace PmSim.Shared.Contracts.Actions
{
    public class SendMessageToEveryoneActionRequest : ActionRequest
    {
        public string Message { get; set; }
    }
}