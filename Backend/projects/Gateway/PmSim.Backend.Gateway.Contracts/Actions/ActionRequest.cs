namespace PmSim.Backend.Gateway.Contracts.Actions
{

    public class ActionRequest
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public string PlayerToken { get; set; }
    }
}