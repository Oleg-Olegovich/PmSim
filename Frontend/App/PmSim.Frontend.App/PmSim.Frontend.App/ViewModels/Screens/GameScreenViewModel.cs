using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Frames;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
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
                MainAreaContent = new ChoosingBackgroundDialogViewModel(this);
            }
        }
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

    public ObservableCollection<PlayerStatus> Players { get; } = new();
    
    private IList<PlayerStatus>? _selectedPlayers;
    
    public IList<PlayerStatus>? SelectedPlayers
    {
        get => _selectedPlayers;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayers, value);
    }

    private ViewModelBase? _mainAreaContent;
    
    public ViewModelBase? MainAreaContent
    {
        get => _mainAreaContent;
        set => this.RaiseAndSetIfChanged(ref _mainAreaContent, value);
    }

    private bool _isOut;

    public bool IsOut
    {
        get => _isOut;
        set => this.RaiseAndSetIfChanged(ref _isOut, value);
    }
    
    public ReactiveCommand<Unit, Unit> GiveUpCommand { get; }
    
    public ReactiveCommand<Unit, Unit> SkipCommand { get; }

    public GameScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, IPmSimClient client) 
        : base(baseWindow, previous)
    {
        _client = client;
        _gameMap = new GameMap0ViewModel(this);
        MainAreaContent = new ConnectionDialogViewModel();
        GiveUpCommand = ReactiveCommand.Create(GiveUp);
        SkipCommand = ReactiveCommand.Create(SkipMove);
    }

    public void ChooseBackground(Professions selectedBackground)
    {
        _client.SetBackground(selectedBackground);
        ShowMapMenu();
    }

    public void ShowOffice(int officeId) 
        => MainAreaContent = _client.GetOfficeState(officeId) switch
        {
            OfficeStates.Unoccupied => new RentOfficeDialogViewModel(this, 
                _client.GetOffice(officeId)!, officeId),
            OfficeStates.Mine => new OfficeMenuViewModel(this),
            _ => new InformationDialogViewModel(this, 
                LocalizationGameScreen.OfficeIsOccupiedByAnother)
        };

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
        catch (PmSimException exception)
        {
            BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
        }
        finally
        {
            ShowMapMenu();
        }
    }

    public void ShowAuctionHouseMenu()
    {
        if (GameStage != GameStages.Diplomacy)
        {
            return;
        }
        
        throw new System.NotImplementedException();
    }

    public void ShowMapMenu()
        => MainAreaContent = _gameMap;

    public void SetOfficeState(int officeId, OfficeStates officeState)
        => _gameMap.ChangeOfficeImage(officeId, officeState);

    public void ProcessLosing()
    {
        IsOut = true;
        MainAreaContent = new LosingViewModel(this);
    }
        
    private void GiveUp() => _client.GiveUp();

    private void SkipMove() => _client.SkipMove();
}