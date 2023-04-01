using PmSim.Shared.GameEngine.GameLogic.BotStrategies;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Others;

namespace PmSim.Shared.GameEngine.GameLogic
{
    internal class Bot : Player
    {
        private static readonly Random Random = new();
        
        private readonly BotStrategy[] _strategies;
        
        private StrategyTypes _strategyType;

        internal List<int> OfficeIndexes { get; } = new();

        internal Bot(int id, string name, int capital, Game game)
            : base(id, name, ChooseProfession(), capital, new StatusChangeNotifier())
        {
            ChooseStrategy();
            _strategies = new BotStrategy[]{
                new RandomStrategy(game, this),
                new CautiousStrategy(game, this),
                new RiskyStrategy(game, this),
                new DiplomaticStrategy(game, this)
            };
        }

        internal void MakeSprintActions()
        {
            if (IsOut)
            {
                return;
            }
            if (IsStartupOpen)
            { 
                _strategies[(int)_strategyType].MakeSprintMove();
                return;
            }
            _strategies[(int)_strategyType].WorkForHire();
        }

        internal void MakeDiplomaticActions()
        {
            if (IsOut)
            {
                return;
            }
            _strategies[(int)_strategyType].MakeDiplomaticMove();
        }
        
        internal void MakeIncidentDecision()
        {
            if (IsOut)
            {
                return;
            }
            _strategies[(int)_strategyType].MakeIncidentDecision();
        }

        private void ChooseStrategy()
        {
            _strategyType = (StrategyTypes)Random.Next(Enum.GetNames(_strategyType.GetType()).Length);
        }
        
        private static Professions ChooseProfession()
            => (Professions)Random.Next(Enum.GetNames(typeof(Professions)).Length);
    }
}