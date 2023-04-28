using PmSim.Shared.Contracts.Credentials;

namespace PmSim.Shared.Contracts.Actions
{

    public class ActionRequest : BasicRequest
    {
        public int GameId { get; set; }
    }
}