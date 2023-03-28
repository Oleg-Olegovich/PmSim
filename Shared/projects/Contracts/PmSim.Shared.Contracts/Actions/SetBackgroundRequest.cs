using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.Contracts.Actions
{

    public class SetBackgroundRequest : ActionRequest
    {
        public Professions Profession { get; set; }
    }
}