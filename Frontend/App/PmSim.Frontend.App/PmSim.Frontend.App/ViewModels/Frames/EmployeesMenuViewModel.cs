using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia.Controls.Selection;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game.Employees;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class EmployeesMenuViewModel : BasicFrameViewModel
{
    private bool _isSelectionNonEmpty;

    public bool IsSelectionNonEmpty
    {
        get => _isSelectionNonEmpty;
        set => this.RaiseAndSetIfChanged(ref _isSelectionNonEmpty, value);
    }
    
    public ObservableCollection<EmployeeStatus> Employees { get; } = new();

    public SelectionModel<EmployeeStatus> Selection { get; }
    
    public ReactiveCommand<Unit, Unit> InterviewCommand { get; }
    
    public ReactiveCommand<Unit, Unit> DevelopmentCommand { get; }
    
    public ReactiveCommand<Unit, Unit> BackUpCommand { get; }
    
    public ReactiveCommand<Unit, Unit> InventionCommand { get; }
    
    public ReactiveCommand<Unit, Unit> CancelTaskCommand { get; }
    
    public ReactiveCommand<Unit, Unit> DismissalCommand { get; }
    
    public EmployeesMenuViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen)
    {
        //Employees.Add(new EmployeeStatus() { Name = "Oleg", Salary = 10, Skills = new SkillsPoints(1, 1, 1, 1, 1), TaskTypeName = "Работает"});
        Selection = new SelectionModel<EmployeeStatus>();
        Selection.SelectionChanged += ProcessSelectionChanged;
        InterviewCommand = ReactiveCommand.Create(ProcessInterviewCommand);
        DevelopmentCommand = ReactiveCommand.Create(ProcessDevelopmentCommand);
        BackUpCommand =ReactiveCommand.Create(ProcessBackUpCommand) ;
        InventionCommand = ReactiveCommand.Create(ProcessInventionCommand);
        CancelTaskCommand = ReactiveCommand.Create(ProcessCancelTaskCommand);
        DismissalCommand = ReactiveCommand.Create(ProcessDismissCommand);
    }

    private void ProcessSelectionChanged(object? sender, SelectionModelSelectionChangedEventArgs e)
        => IsSelectionNonEmpty = Selection.Count > 0;
    
    private void ProcessInterviewCommand()
    {
        GameScreen.MainAreaContent = new InterviewDialogViewModel(GameScreen);
        GameScreen.CurrentTabIndex = 0;
    }
    
    private void ProcessDevelopmentCommand()
    {
        GameScreen.MainAreaContent = new SelectProjectViewModel(GameScreen);
        GameScreen.CurrentTabIndex = 0;
    }
    
    private void ProcessBackUpCommand()
    {
        GameScreen.MainAreaContent = new SelectProjectViewModel(GameScreen);
        GameScreen.CurrentTabIndex = 0;
    }
    
    private void ProcessInventionCommand()
    {
        
    }

    private void ProcessCancelTaskCommand()
    {
        foreach (var employee in Selection.SelectedItems)
        {
            employee.TaskType = EmployeeTaskTypes.Non;
            employee.TaskTypeName = "";
        }
    }
    
    private void ProcessDismissCommand()
    {
        
    }
}