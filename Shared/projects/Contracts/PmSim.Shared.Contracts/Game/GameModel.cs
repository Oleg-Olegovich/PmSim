using PmSim.Shared.Contracts.Enums;

namespace PmSim.Shared.Contracts.Game;

public class GameModel
{
    public int Id { get; }
    
    public string Name { get; }
    
    public string Founder { get; }
    
    public GameModes Mode { get; }
    
    public string? ModeName { get; set; }

    public GameMaps Map { get; }

    public string? MapName { get; set; }
    
    public int Players { get; }
    
    public int MaxPlayers { get; }

    public GameModel(int id, string founder, string name, GameModes mode, GameMaps map, int players, int mapPlayers)
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