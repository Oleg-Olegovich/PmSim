namespace StartupSim.Backend.Gateway.Contracts.Actions
{
    public class SendMessageToEveryoneActionRequest : ActionRequest
    {
        public string Message { get; set; }
    }
}