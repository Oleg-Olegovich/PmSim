namespace PmSim.Shared.GameEngine.GameLogic.BotStrategies;

internal abstract class BotStrategy
{
    protected readonly Random Random = new();
    protected readonly Game Game;
    protected readonly Bot Bot;
        
    protected BotStrategy(Game game, Bot bot)
    {
        Game = game;
        Bot = bot;
    }
        
    internal abstract void WorkForHire();
    internal abstract void MakeSprintMove();
    internal abstract void MakeDiplomaticMove();
    internal abstract void MakeIncidentDecision();
}