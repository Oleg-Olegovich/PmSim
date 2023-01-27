using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StartupSim.Backend.GameEngine.Enums;
using StartupSim.Backend.GameEngine.GameLogic.BotStrategies;

namespace StartupSim.Backend.GameEngine.GameLogic
{
    public class Bot : Player
    {
        private static readonly Random Random = new Random();
        
        private readonly BotStrategy[] _strategies;
        
        private StrategyTypes _strategyType;

        public List<int> OfficeIndexes { get; } = new List<int>();

        public Bot(int id, string name, int capital, Game game)
            : base(id, name, ChooseProfession(), capital)
        {
            ChooseStrategy();
            _strategies = new BotStrategy[]{
                new RandomStrategy(game, this),
                new CautiousStrategy(game, this),
                new RiskyStrategy(game, this),
                new DiplomaticStrategy(game, this)
            };
        }

        public async Task MakeSprintActions()
        {
            if (IsOut)
            {
                return;
            }
            if (IsStartupOpen)
            { 
                await _strategies[(int)_strategyType].MakeSprintMove();
                return;
            }
            await _strategies[(int)_strategyType].WorkForHire();
        }

        public async Task MakeDiplomaticActions()
        {
            if (IsOut)
            {
                return;
            }
            await _strategies[(int)_strategyType].MakeDiplomaticMove();
        }
        
        public async Task MakeIncidentDecision()
        {
            if (IsOut)
            {
                return;
            }
            await _strategies[(int)_strategyType].MakeIncidentDecision();
        }

        private void ChooseStrategy()
        {
            _strategyType = (StrategyTypes)Random.Next(Enum.GetNames(_strategyType.GetType()).Length);
        }
        
        private static Professions ChooseProfession()
            => (Professions)Random.Next(Enum.GetNames(typeof(Professions)).Length);
    }
}