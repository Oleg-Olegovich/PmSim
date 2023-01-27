using System;
using System.Threading.Tasks;

namespace StartupSim.Backend.GameEngine.GameLogic.BotStrategies
{
    public abstract class BotStrategy
    {
        protected readonly Random _random = new Random();
        protected readonly Game Game;
        protected readonly Bot Bot;
        
        protected BotStrategy(Game game, Bot bot)
        {
            Game = game;
            Bot = bot;
        }
        
        public abstract Task WorkForHire();
        public abstract Task MakeSprintMove();
        public abstract Task MakeDiplomaticMove();
        public abstract Task MakeIncidentDecision();
    }
}