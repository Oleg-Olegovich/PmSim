using System.Collections.ObjectModel;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game.Employees;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class EmployeesMenuViewModel : BasicFrameViewModel
{
    public ObservableCollection<EmployeeStatus> Employees { get; } = new();

    public EmployeesMenuViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen)
    {
        Employees.Add(new EmployeeStatus() { Name = "Oleg", Salary = 10, Skills = new SkillsPoints(1, 1, 1, 1, 1), TaskTypeName = "Работает"});
        Employees.Add(new EmployeeStatus() { Name = "Oleg", Salary = 10, Skills = new SkillsPoints(1, 1, 1, 1, 1), TaskTypeName = "Работает"});
    }
}