using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GameOptionsScreenViewModel : BasicScreenViewModel
{
    // Option fields.
    
    private string _name;
    
    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }
    
    private int _selectedMode;
    
    public int SelectedMode
    {
        get => _selectedMode;
        set => this.RaiseAndSetIfChanged(ref _selectedMode, value);
    }
    
    private int _selectedMap;
    
    public int SelectedMap
    {
        get => _selectedMap;
        set => this.RaiseAndSetIfChanged(ref _selectedMap, value);
    }
    
    private int _connectionRealTime;
    
    public int ConnectionRealTime
    {
        get => _connectionRealTime;
        set => this.RaiseAndSetIfChanged(ref _connectionRealTime, value);
    }
    
    private int _choosingBackgroundRealTime;
    
    public int ChoosingBackgroundRealTime
    {
        get => _choosingBackgroundRealTime;
        set => this.RaiseAndSetIfChanged(ref _choosingBackgroundRealTime, value);
    }
    
    private int _sprintRealTime;
    
    public int SprintRealTime
    {
        get => _sprintRealTime;
        set => this.RaiseAndSetIfChanged(ref _sprintRealTime, value);
    }
    
    private int _diplomacyRealTime;
    
    public int DiplomacyRealTime
    {
        get => _diplomacyRealTime;
        set => this.RaiseAndSetIfChanged(ref _diplomacyRealTime, value);
    }
    
    private int _incidentRealTime;
    
    public int IncidentRealTime
    {
        get => _incidentRealTime;
        set => this.RaiseAndSetIfChanged(ref _incidentRealTime, value);
    }

    private int _auctionRealTime;
    
    public int AuctionRealTime
    {
        get => _auctionRealTime;
        set => this.RaiseAndSetIfChanged(ref _auctionRealTime, value);
    }
    
    private int _sprintActionsNumber;
    
    public int SprintActionsNumber
    {
        get => _sprintActionsNumber;
        set => this.RaiseAndSetIfChanged(ref _sprintActionsNumber, value);
    }
    
    private int _startUpCapital;
    
    public int StartUpCapital
    {
        get => _startUpCapital;
        set => this.RaiseAndSetIfChanged(ref _startUpCapital, value);
    }
    
    // Other field.
    
    private string[] _modes;
    
    public string[] Modes
    {
        get => _modes;
        set => this.RaiseAndSetIfChanged(ref _modes, value);
    }
    
    private string[] _maps;
    
    public string[] Maps
    {
        get => _maps;
        set => this.RaiseAndSetIfChanged(ref _maps, value);
    }
    
    public ReactiveCommand<Unit, Unit> DefaultCommand { get; }

    public GameOptionsScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, PmSimClient client) 
        : base(baseWindow, previous, new GameScreenViewModel(baseWindow, previous, client))
    {
        Modes = client.GetModes();
        Maps = client.GetMaps();
        ProcessDefaultClick();
        DefaultCommand = ReactiveCommand.Create(ProcessDefaultClick);
    }

    private void ProcessDefaultClick()
    {
        Name = "Default";
        SelectedMode = 0;
        SelectedMap = 0;
        ConnectionRealTime = 60;
        ChoosingBackgroundRealTime = 60;
        SprintRealTime = 180;
        DiplomacyRealTime = 180;
        IncidentRealTime = 60;
        AuctionRealTime = 10;
        SprintActionsNumber = 2;
        StartUpCapital = 10;
    }
}