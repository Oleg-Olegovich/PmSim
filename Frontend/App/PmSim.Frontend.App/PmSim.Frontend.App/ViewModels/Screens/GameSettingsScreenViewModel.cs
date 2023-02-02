using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GameSettingsScreenViewModel : BasicScreenViewModel
{
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
    
    public ReactiveCommand<Unit, Unit> DefaultCommand { get; }

    public GameSettingsScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous) 
        : base(baseWindow, previous, new GameScreenViewModel(baseWindow, previous))
    {
        ProcessDefaultClick();
        DefaultCommand = ReactiveCommand.Create(ProcessDefaultClick);
    }

    private void ProcessDefaultClick()
    {
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