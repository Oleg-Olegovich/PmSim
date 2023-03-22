namespace PmSim.Frontend.Client.Dto;

public class Game
{
    public int Id { get; }
    
    public string Name { get; }
    
    public string Mode { get; }

    public string Map { get; }
    
    public int Players { get; }
    
    public int MaxPlayers { get; }

    public Game(int id, string name, string mode, string map, int players, int mapPlayers)
    {
        Id = id;
        Mode = mode;
        Name = name;
        Map = map;
        Players = players;
        MaxPlayers = mapPlayers;
    }
}