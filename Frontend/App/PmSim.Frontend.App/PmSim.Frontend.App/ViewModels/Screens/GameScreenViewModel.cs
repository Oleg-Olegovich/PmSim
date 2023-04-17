using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Frames;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;
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

    private int _currentProjectsNumber;

    public int CurrentProjectsNumber
    {
        get => _currentProjectsNumber;
        set => this.RaiseAndSetIfChanged(ref _currentProjectsNumber, value);
    }
    
    private int _ideasNumber;

    public int IdeasNumber
    {
        get => _ideasNumber;
        set => this.RaiseAndSetIfChanged(ref _ideasNumber, value);
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

    public OpportunitiesMenuViewModel OpportunitiesMenu { get; }
    
    private AuctionMenuViewModel AuctionsMenu { get; }

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
        OpportunitiesMenu = new OpportunitiesMenuViewModel(this);
        AuctionsMenu = new AuctionMenuViewModel(this);
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
        MainAreaContent = state == OfficeStates.NotMine 
            ? new InformationDialogViewModel(this, LocalizationGameScreen.OfficeIsOccupiedByAnother) 
            : new RentOfficeDialogViewModel(this, _client.GetOffice(officeId)!, officeId);
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

    public void CancelOfficeLease(int officeId)
    {
        try
        {
            _client.CancelOfficeLease(officeId);
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

    public void Add(EmployeeStatus employee)
    {
        EmployeesMenu.Employees.Add(employee);
        ++EmployeesNumber;
    }

    public void Remove(EmployeeStatus employee)
    {
        EmployeesMenu.Employees.Remove(employee);
        --EmployeesNumber;
    }
    
    public void Add(Project project)
    {
        ProjectsMenu.Projects.Add(project);
        ProjectsMenu.Ideas.Add(project);
        ++IdeasNumber;
    }

    public void RequestStartProject(int id)
        => _client.RequestStartProject(id);
    
    public void StartProject(int id)
    {
        ProjectsMenu.Ideas.Remove(ProjectsMenu.Projects[id]);
        ProjectsMenu.CurrentProjects.Add(ProjectsMenu.Projects[id]);
        ProjectsMenu.Projects[id].IsStart = true;
        --IdeasNumber;
        ++CurrentProjectsNumber;
    }

    public void CompleteProject(int id)
    {
        ProjectsMenu.CurrentProjects.Remove(ProjectsMenu.Projects[id]);
        ProjectsMenu.CompletedProjects.Add(ProjectsMenu.Projects[id]);
        --CurrentProjectsNumber;
        ++CompletedProjectsNumber;
    }

    public void FailProject(int id)
    {
        ProjectsMenu.CurrentProjects.Remove(ProjectsMenu.Projects[id]);
        ProjectsMenu.FailedProjects.Add(ProjectsMenu.Projects[id]);
        --CurrentProjectsNumber;
        ++FailedProjectsNumber;
    }

    public void UpdateProjectProgress(int id, ProgressPoints points) 
        => ProjectsMenu.Projects[id].Points = points;

    public void AddOpportunity(OpportunityModel opportunity) 
        => OpportunitiesMenu.Opportunities.Add(opportunity);

    public void RemoveOpportunity(int number)
    {
        var opportunity = OpportunitiesMenu.Opportunities
            .FirstOrDefault(x => x.Number == number);
        if (opportunity is not null)
        {
            OpportunitiesMenu.Opportunities.Remove(opportunity);
        }
    }

    public void AssignToDevelop()
    {
        _client.AssignToDevelop(0, 0, Professions.Designer);
    }

    public void ProcessLosing()
    {
        IsFinish = true;
        ShowSkipButton = ShowGiveUpButton = false;
        MainAreaContent = new InformationDialogViewModel(this, LocalizationGameScreen.Losing);
        CurrentTabIndex = 0;
    }

    private void GiveUp() => _client.GiveUp();

    private void SkipMove() => _client.SkipMove();

    private void ExitToTitleScreen()
        => BaseWindow.Content = new TitleScreenViewModel(BaseWindow);
}