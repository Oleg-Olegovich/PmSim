using PmSim.Backend.Gateway.Contracts.Enums;

namespace PmSim.Backend.Gateway.Contracts.Actions
{

    public class SetBackgroundRequest : ActionRequest
    {
        public Professions Profession { get; set; }
    }
}