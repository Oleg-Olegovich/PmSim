using System.Reactive;
using System.Threading.Tasks;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Api;
using PmSim.Frontend.Client.Dto;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GameOptionsScreenViewModel : BasicScreenViewModel
{
    private readonly IPmSimClient _client;
    
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
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _playersNumber, value);
            }
        }
    }

    private int _botsNumber;
    
    public int BotsNumber
    {
        get => _botsNumber;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _botsNumber, value);
            }
        }
    }

    private int _connectionRealTime;
    
    public int ConnectionRealTime
    {
        get => _connectionRealTime;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _connectionRealTime, value);
            }
        }
    }

    private int _choosingBackgroundRealTime;
    
    public int ChoosingBackgroundRealTime
    {
        get => _choosingBackgroundRealTime;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _choosingBackgroundRealTime, value);
            }
        }
    }

    private int _sprintRealTime;
    
    public int SprintRealTime
    {
        get => _sprintRealTime;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _sprintRealTime, value);
            }
        }
    }

    private int _diplomacyRealTime;
    
    public int DiplomacyRealTime
    {
        get => _diplomacyRealTime;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _diplomacyRealTime, value);
            }
        }
    }

    private int _incidentRealTime;
    
    public int IncidentRealTime
    {
        get => _incidentRealTime;
        set {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _incidentRealTime, value);
            }
        }
    }

    private int _auctionRealTime;
    
    public int AuctionRealTime
    {
        get => _auctionRealTime;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _auctionRealTime, value);
            }
        }
    }

    private int _sprintActionsNumber;
    
    public int SprintActionsNumber
    {
        get => _sprintActionsNumber;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _sprintActionsNumber, value);
            }
        }
    }

    private int _startUpCapital;
    
    public int StartUpCapital
    {
        get => _startUpCapital;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _startUpCapital, value);
            }
        }
    }

    // Other field.
    
    public bool IsSingleplayer { get; }
    
    public string[] Modes { get; }
    
    public string[] Maps { get; }
    
    public ReactiveCommand<Unit, Unit> DefaultCommand { get; }
    
    public ReactiveCommand<Unit, Unit> StartCommand { get; }

    public GameOptionsScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, 
        IPmSimClient client, bool isSingleplayer) : base(baseWindow, previous)
    {
        _client = client;
        IsSingleplayer = isSingleplayer;
        Modes = IPmSimClient.GetModes();
        Maps = IPmSimClient.GetMaps();
        ProcessDefaultClick();
        DefaultCommand = ReactiveCommand.Create(ProcessDefaultClick);
        StartCommand = ReactiveCommand.Create(StartGame);
    }

    private void StartGame()
    {
        var settings = new GameOptions(Name, PlayersNumber, BotsNumber, (GameModes)SelectedMode, SelectedMap, 
            ConnectionRealTime, ChoosingBackgroundRealTime, SprintRealTime, DiplomacyRealTime, IncidentRealTime, 
            SprintActionsNumber, AuctionRealTime, StartUpCapital);
        try
        {
            var gameScreen = new GameScreenViewModel(BaseWindow, new TitleScreenViewModel(BaseWindow), _client);
            _client.CreateNewGame(settings, gameScreen);
            BaseWindow.Content = gameScreen;
        }
        catch (PmSimException exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
    }
    
    private void ProcessDefaultClick()
    {
        var defaultSettings = GameOptions.Default;
        Name = defaultSettings.GameName;
        PlayersNumber = IsSingleplayer ? 1 : defaultSettings.MaxPlayersNumber;
        BotsNumber = defaultSettings.BotsNumber;
        SelectedMode = (int)defaultSettings.Mode;
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