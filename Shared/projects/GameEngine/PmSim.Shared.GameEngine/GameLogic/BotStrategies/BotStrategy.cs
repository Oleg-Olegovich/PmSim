using System;
using System.Threading.Tasks;

namespace PmSim.Shared.GameEngine.GameLogic.BotStrategies
{
    internal abstract class BotStrategy
    {
        protected readonly Random _random = new Random();
        protected readonly Game Game;
        protected readonly Bot Bot;
        
        protected BotStrategy(Game game, Bot bot)
        {
            Game = game;
            Bot = bot;
        }
        
        internal abstract Task WorkForHire();
        internal abstract Task MakeSprintMove();
        internal abstract Task MakeDiplomaticMove();
        internal abstract Task MakeIncidentDecision();
    }
}