using System;
using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Enums;
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
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedMode, value);
            ShowMaxSprintNumber = value > 0;
        }
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

    private int _managementActionsNumber;
    
    public int ManagementActionsNumber
    {
        get => _managementActionsNumber;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _managementActionsNumber, value);
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

    private int _maxSprintNumber;
    
    public int MaxSprintNumber
    {
        get => _maxSprintNumber;
        set
        {
            if (value != 0)
            {
                this.RaiseAndSetIfChanged(ref _maxSprintNumber, value);
            }
        }
    }

    private bool _showMaxSprintNumber;

    public bool ShowMaxSprintNumber
    {
        get => _showMaxSprintNumber;
        set => this.RaiseAndSetIfChanged(ref _showMaxSprintNumber, value);
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
            ConnectionRealTime, ChoosingBackgroundRealTime, SprintRealTime, IncidentRealTime, 
            ManagementActionsNumber, AuctionRealTime, StartUpCapital, MaxSprintNumber);
        try
        {
            var gameScreen = new GameScreenViewModel(BaseWindow, new TitleScreenViewModel(BaseWindow), _client);
            _client.CreateNewGame(settings, gameScreen);
            BaseWindow.Content = gameScreen;
            BaseWindow.Logger.Information("Showed GameScreen");
        }
        catch (Exception exception)
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
        IncidentRealTime = defaultSettings.IncidentRealTime;
        AuctionRealTime = defaultSettings.AuctionRealTime;
        ManagementActionsNumber = defaultSettings.ManagementActionsNumbers;
        StartUpCapital = defaultSettings.StartUpCapital;
        MaxSprintNumber = defaultSettings.MaxSprintNumber;
    }
}