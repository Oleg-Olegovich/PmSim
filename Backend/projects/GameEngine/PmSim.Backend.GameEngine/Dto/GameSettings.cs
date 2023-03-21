using PmSim.Backend.Gateway.Contracts.Enums;

namespace PmSim.Backend.GameEngine.Dto
{

    public class GameSettings
    {
        public int MapNumber { get; }

        public int ConnectionRealTime { get; }

        public int ChoosingBackgroundRealTime { get; }

        public int SprintRealTime { get; }

        public int DiplomacyRealTime { get; }

        public int IncidentRealTime { get; }

        public int AuctionRealTime { get; }

        public int SprintActionsNumbers { get; }

        public int StartUpCapital { get; }

        public GameModes Mode { get; }

        public GameSettings(int mode)
        {
            Mode = (GameModes)mode;
            MapNumber = 0;
            ConnectionRealTime = GameConstants.DefaultConnectionRealTime;
            ChoosingBackgroundRealTime = GameConstants.DefaultChoosingBackground;
            SprintRealTime = GameConstants.DefaultSprintRealTime;
            DiplomacyRealTime = GameConstants.DefaultDiplomacyRealTime;
            IncidentRealTime = GameConstants.DefaultIncidentRealTime;
            SprintActionsNumbers = GameConstants.DefaultSprintActionsNumbers;
            AuctionRealTime = GameConstants.DefaultAuctionRealTime;
            StartUpCapital = GameConstants.DefaultStartUpCapital;
        }

        public GameSettings(int mode, int connection, int background, int sprint,
            int diplomacy, int incident, int auction, int sprintActions, int capital, int mapNumber)
        {
            Mode = (GameModes)mode;
            ChoosingBackgroundRealTime = background;
            ConnectionRealTime = connection;
            SprintRealTime = sprint;
            DiplomacyRealTime = diplomacy;
            IncidentRealTime = incident;
            SprintActionsNumbers = sprintActions;
            AuctionRealTime = auction;
            StartUpCapital = capital;
            MapNumber = mapNumber;
        }
    }
}