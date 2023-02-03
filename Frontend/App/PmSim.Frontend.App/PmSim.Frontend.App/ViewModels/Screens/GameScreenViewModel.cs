using PmSim.Frontend.App.ViewModels.Frames;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GameScreenViewModel : BasicScreenViewModel
{
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
    
    private int _actionsNumber = 0;
    
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

    private ViewModelBase? _mainAreaContent;
    
    public ViewModelBase? MainAreaContent
    {
        get => _mainAreaContent;
        set => this.RaiseAndSetIfChanged(ref _mainAreaContent, value);
    }
    
    public GameScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous) 
        : base(baseWindow, previous)
    {
        MainAreaContent = new GameMap0ViewModel();
    }
}