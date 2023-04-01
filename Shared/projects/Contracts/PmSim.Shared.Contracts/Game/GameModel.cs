namespace PmSim.Shared.Contracts.Game;

public class GameModel
{
    public int Id { get; }
    
    public string Name { get; }
    
    public string Founder { get; }
    
    public string Mode { get; }

    public string Map { get; }
    
    public int Players { get; }
    
    public int MaxPlayers { get; }

    public GameModel(int id, string founder, string name, string mode, string map, int players, int mapPlayers)
    {
        Id = id;
        Founder = founder;
        Mode = mode;
        Name = name;
        Map = map;
        Players = players;
        MaxPlayers = mapPlayers;
    }
}