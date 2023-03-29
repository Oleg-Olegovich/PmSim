using PmSim.Shared.Contracts.Game;

namespace PmSim.Frontend.Client.Api;

public class SingleplayerClient : IPmSimClient
{
    private readonly string _playerName;
    
    public SingleplayerClient(string playerName)
    {
        _playerName = playerName;
    }

    public async Task CreateNewGameAsync(GameOptions gameOptions)
    {
        
    }
}