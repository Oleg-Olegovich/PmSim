namespace PmSim.Backend.Gateway.Contracts.Actions
{
    public class SendMessageToEveryoneActionRequest : ActionRequest
    {
        public string Message { get; set; }
    }
}