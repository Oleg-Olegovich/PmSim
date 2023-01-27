using StartupSim.Backend.GameEngine.Enums;

namespace StartupSim.Backend.Gateway.Contracts.Actions
{

    public class SetBackgroundRequest : ActionRequest
    {
        public Professions Profession { get; set; }
    }
}