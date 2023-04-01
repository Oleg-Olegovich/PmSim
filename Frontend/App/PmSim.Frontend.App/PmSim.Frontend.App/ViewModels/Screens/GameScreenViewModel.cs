using System.Collections.ObjectModel;
using System.Reactive;
using PmSim.Frontend.App.ViewModels.Frames;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.App.Views.Frames;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Interfaces;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GameScreenViewModel : BasicScreenViewModel, IGameScreenLogic
{
    private readonly IPmSimClient _client;
    
    private string _gameStage = "Game stage";
    
    public string GameStage
    {
        get => _gameStage;
        set => this.RaiseAndSetIfChanged(ref _gameStage, value);
    }
    
    private string _time = "00:00";
    
    public string Time
    {
        get => _time;
        set => this.RaiseAndSetIfChanged(ref _time, value);
    }
    
    private int _actionsNumber;
    
    public int ActionsNumber
    {
        get => _actionsNumber;
        set => this.RaiseAndSetIfChanged(ref _actionsNumber, value);
    }
    
    private int _money;
    
    public int Money
    {
        get => _money;
        set => this.RaiseAndSetIfChanged(ref _money, value);
    }
    
    private int _officesNumber;
    
    public int OfficesNumber
    {
        get => _officesNumber;
        set => this.RaiseAndSetIfChanged(ref _officesNumber, value);
    }
    
    private int _projectsNumber;
    
    public int ProjectsNumber
    {
        get => _projectsNumber;
        set => this.RaiseAndSetIfChanged(ref _projectsNumber, value);
    }
    
    private int _employeesNumber;
    
    public int EmployeesNumber
    {
        get => _employeesNumber;
        set => this.RaiseAndSetIfChanged(ref _employeesNumber, value);
    }

    public ObservableCollection<PlayerStatus> Players { get; set; } = new();
    
    private PlayerStatus? _selectedPlayer;
    
    public PlayerStatus? SelectedPlayer
    {
        get => _selectedPlayer;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayer, value);
    }

    private ViewModelBase? _mainAreaContent;
    
    public ViewModelBase? MainAreaContent
    {
        get => _mainAreaContent;
        set => this.RaiseAndSetIfChanged(ref _mainAreaContent, value);
    }
    
    public ReactiveCommand<Unit, Unit> GiveUpCommand { get; }
    
    public ReactiveCommand<Unit, Unit> SkipCommand { get; }

    public GameScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, IPmSimClient client) 
        : base(baseWindow, previous)
    {
        _client = client;
        MainAreaContent = new ChoosingBackgroundDialogViewModel(this);
        GiveUpCommand = ReactiveCommand.Create(GiveUp);
        SkipCommand = ReactiveCommand.Create(SkipMove);
    }

    public void ChooseBackground(Professions selectedBackground)
    {
        _client.SetBackground(selectedBackground);
        MainAreaContent = new GameMap0ViewModel(this);
    }
    
    private void GiveUp()
    {
        _client.ExitGame();
        BaseWindow.Content = new TitleScreenViewModel(BaseWindow);
    }

    private void SkipMove()
        => _client.SkipMove();
}