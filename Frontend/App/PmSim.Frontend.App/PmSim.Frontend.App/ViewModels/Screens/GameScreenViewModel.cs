using System;
using System.Collections.ObjectModel;
using System.Reactive;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Frames;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Interfaces;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GameScreenViewModel : BasicScreenViewModel, IGameScreenLogic
{
    private readonly IPmSimClient _client;

    private readonly BasicGameMapViewModel _gameMap;

    private GameStages _gameStage;

    public GameStages GameStage
    {
        get => _gameStage;
        set
        {
            _gameStage = value;
            if (_gameStage == GameStages.ChoosingBackground)
            {
                IsFinish = false;
                MainAreaContent = new ChoosingBackgroundDialogViewModel(this);
            }
            if (IsFinish)
            {
                return;
            }

            ShowGiveUpButton = _gameStage != GameStages.Connection;
            ShowSkipButton = _gameStage == GameStages.Management;
        }
    }

    private string _sprintNumberLine = $"{LocalizationGameScreen.Sprint} {1}";

    public string SprintNumberLine
    {
        get => _sprintNumberLine;
        set => this.RaiseAndSetIfChanged(ref _sprintNumberLine, value);
    }

    public int SprintNumber
    {
        set => SprintNumberLine = $"{LocalizationGameScreen.Sprint} {value}";
    }
        
    private string _gameStageName = "Not started";

    public string GameStageName
    {
        get => _gameStageName;
        set => this.RaiseAndSetIfChanged(ref _gameStageName, value);
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

    private int _techSupportOfficersNumber;

    public int TechSupportOfficersNumber
    {
        get => _techSupportOfficersNumber;
        set => this.RaiseAndSetIfChanged(ref _techSupportOfficersNumber, value);
    }

    private int _projectsNumber;

    public int ProjectsNumber
    {
        get => _projectsNumber;
        set => this.RaiseAndSetIfChanged(ref _projectsNumber, value);
    }

    private int _completedProjectsNumber;

    public int CompletedProjectsNumber
    {
        get => _completedProjectsNumber;
        set => this.RaiseAndSetIfChanged(ref _completedProjectsNumber, value);
    }

    private int _failedProjectsNumber;

    public int FailedProjectsNumber
    {
        get => _failedProjectsNumber;
        set => this.RaiseAndSetIfChanged(ref _failedProjectsNumber, value);
    }

    private int _employeesNumber;

    public int EmployeesNumber
    {
        get => _employeesNumber;
        set => this.RaiseAndSetIfChanged(ref _employeesNumber, value);
    }

    private int _maxEmployeesNumber;

    public int MaxEmployeesNumber
    {
        get => _maxEmployeesNumber;
        set => this.RaiseAndSetIfChanged(ref _maxEmployeesNumber, value);
    }

    public ObservableCollection<PlayerStatus> Players { get; set; } = new();

    private ViewModelBase? _mainAreaContent;

    public ViewModelBase? MainAreaContent
    {
        get => _mainAreaContent;
        set => this.RaiseAndSetIfChanged(ref _mainAreaContent, value);
    }
    
    public EmployeesMenuViewModel EmployeesMenu { get; }
    
    public ProjectsMenuViewModel ProjectsMenu { get; }

    public OpportunityMenuViewModel OpportunitiesMenu { get; }
    
    private AuctionMenuViewModel AuctionMenu { get; }

    private bool _showSkipButton;

    public bool ShowSkipButton
    {
        get => _showSkipButton;
        set => this.RaiseAndSetIfChanged(ref _showSkipButton, value);
    }

    private bool _showGiveUpButton;

    public bool ShowGiveUpButton
    {
        get => _showGiveUpButton;
        set => this.RaiseAndSetIfChanged(ref _showGiveUpButton, value);
    }

    private bool _isFinish = true;

    public bool IsFinish
    {
        get => _isFinish;
        set => this.RaiseAndSetIfChanged(ref _isFinish, value);
    }

    private int _currentTabIndex;

    public int CurrentTabIndex
    {
        get => _currentTabIndex;
        set => this.RaiseAndSetIfChanged(ref _currentTabIndex, value);
    }

    public ReactiveCommand<Unit, Unit> GiveUpCommand { get; }

    public ReactiveCommand<Unit, Unit> SkipCommand { get; }

    public ReactiveCommand<Unit, Unit> ExitCommand { get; }

    public GameScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, IPmSimClient client)
        : base(baseWindow, previous)
    {
        _client = client;
        _gameMap = new GameMap0ViewModel(this);
        EmployeesMenu = new EmployeesMenuViewModel(this);
        ProjectsMenu = new ProjectsMenuViewModel(this);
        OpportunitiesMenu = new OpportunityMenuViewModel(this);
        AuctionMenu = new AuctionMenuViewModel(this);
        MainAreaContent = new ConnectionDialogViewModel();
        GiveUpCommand = ReactiveCommand.Create(GiveUp);
        SkipCommand = ReactiveCommand.Create(SkipMove);
        ExitCommand = ReactiveCommand.Create(ExitToTitleScreen);
    }

    public void ChooseBackground(Professions selectedBackground)
    {
        _client.SetBackground(selectedBackground);
        ShowMapMenu();
    }

    public void ShowOffice(int officeId)
    {
        var state = _client.GetOfficeState(officeId);
        if (state is OfficeStates.Mine)
        {
            CurrentTabIndex = 1;
            return;
        }
        
        MainAreaContent = state == OfficeStates.Unoccupied 
            ? new RentOfficeDialogViewModel(this, _client.GetOffice(officeId)!, officeId) 
            : new InformationDialogViewModel(this, LocalizationGameScreen.OfficeIsOccupiedByAnother);
    }

    public void RentOffice(int officeId, int rentalPrice)
    {
        if (Money < rentalPrice)
        {
            MainAreaContent = new InformationDialogViewModel(this, LocalizationGameScreen.NotEnoughMoney);
            return;
        }
        
        try
        {
            _client.RentOffice(officeId);
        }
        catch (Exception exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
        finally
        {
            ShowMapMenu();
        }
    }

    public void ShowAuctionHouseMenu() => CurrentTabIndex = 4;

    public void ShowMapMenu() => MainAreaContent = IsFinish ? null : _gameMap;

    public void SetOfficeState(int officeId, OfficeStates officeState)
    {
        try
        {
            _gameMap.ChangeOfficeImage(officeId, officeState);
        }
        catch (Exception exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
    }

    public void ProcessLosing()
    {
        IsFinish = true;
        ShowSkipButton = ShowGiveUpButton = false;
        MainAreaContent = new InformationDialogViewModel(this, LocalizationGameScreen.Losing);
    }

    private void GiveUp() => _client.GiveUp();

    private void SkipMove() => _client.SkipMove();

    private void ExitToTitleScreen()
        => BaseWindow.Content = new TitleScreenViewModel(BaseWindow);
}