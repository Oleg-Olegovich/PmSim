using PmSim.Backend.GameEngine.Enums;

namespace PmSim.Backend.Gateway.Contracts.Actions
{

    public class SetBackgroundRequest : ActionRequest
    {
        public Professions Profession { get; set; }
    }
}