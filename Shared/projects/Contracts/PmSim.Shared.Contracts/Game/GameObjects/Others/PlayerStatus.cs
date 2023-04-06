using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace PmSim.Shared.Contracts.Game.GameObjects.Others;

/// <summary>
/// Players or bots status (property, money).
/// </summary>
public sealed class PlayerStatus : INotifyPropertyChanged
{
    [JsonPropertyName("id")] 
    public int Id { get; set; }

    [JsonPropertyName("name")] 
    public string? Name { get; set; }
    
    private int _money;

    [JsonPropertyName("money")]
    public int Money
    {
        get => _money;
        set
        {
            if (value == _money)
            {
                return;
            }
            
            _money = value;
            OnPropertyChanged();
        }
    }
    
    private int _completedProjects;
    
    [JsonPropertyName("completedProjects")]
    public int CompletedProjects
    {
        get => _completedProjects;
        set
        {
            if (value == _completedProjects)
            {
                return;
            }
            
            _completedProjects = value;
            OnPropertyChanged();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}