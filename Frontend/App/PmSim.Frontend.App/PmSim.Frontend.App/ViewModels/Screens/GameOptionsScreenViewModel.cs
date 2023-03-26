using System.Reactive;
using System.Threading.Tasks;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Dto;
using PmSim.Frontend.Client.Exceptions;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GameOptionsScreenViewModel : BasicScreenViewModel
{
    private readonly PmSimClient _client;
    
    // Option fields.
    
    private string _name = "";
    
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
    
    private int _playersNumber;
    
    public int PlayersNumber
    {
        get => _playersNumber;
        set => this.RaiseAndSetIfChanged(ref _playersNumber, value);
    }
    
    private int _botsNumber;
    
    public int BotsNumber
    {
        get => _botsNumber;
        set => this.RaiseAndSetIfChanged(ref _botsNumber, value);
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
    
    public bool IsMultiplayer { get; }
    
    public string[] Modes { get; }
    
    public string[] Maps { get; }
    
    public ReactiveCommand<Unit, Unit> DefaultCommand { get; }
    
    public ReactiveCommand<Unit, Unit> StartCommand { get; }

    public GameOptionsScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, 
        PmSimClient client, bool isMultiplayer) : base(baseWindow, previous)
    {
        _client = client;
        IsMultiplayer = isMultiplayer;
        if (!isMultiplayer)
        {
            PlayersNumber = 1;
        }
        Modes = client.GetModes();
        Maps = client.GetMaps();
        ProcessDefaultClick();
        DefaultCommand = ReactiveCommand.Create(ProcessDefaultClick);
        StartCommand = ReactiveCommand.CreateFromTask(StartGame);
    }

    private async Task StartGame()
    {
        var settings = new GameSettings(Name, PlayersNumber, BotsNumber, SelectedMode, SelectedMap, ConnectionRealTime, 
            ChoosingBackgroundRealTime, SprintRealTime, DiplomacyRealTime, IncidentRealTime, SprintActionsNumber, 
            AuctionRealTime, StartUpCapital);
        try
        {
            await _client.CreateNewGameAsync(settings);
            BaseWindow.Content = new GameScreenViewModel(BaseWindow, new TitleScreenViewModel(BaseWindow), _client);
        }
        catch (PmSimClientException exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
    }
    
    private void ProcessDefaultClick()
    {
        var defaultSettings = GameSettings.Default;
        Name = defaultSettings.GameName;
        PlayersNumber = defaultSettings.MaxPlayersNumber;
        BotsNumber = defaultSettings.BotsNumber;
        SelectedMode = defaultSettings.Mode;
        SelectedMap = defaultSettings.MapNumber;
        ConnectionRealTime = defaultSettings.ConnectionRealTime;
        ChoosingBackgroundRealTime = defaultSettings.ChoosingBackgroundRealTime;
        SprintRealTime = defaultSettings.SprintRealTime;
        DiplomacyRealTime = defaultSettings.DiplomacyRealTime;
        IncidentRealTime = defaultSettings.IncidentRealTime;
        AuctionRealTime = defaultSettings.AuctionRealTime;
        SprintActionsNumber = defaultSettings.SprintRealTime;
        StartUpCapital = defaultSettings.StartUpCapital;
    }
}