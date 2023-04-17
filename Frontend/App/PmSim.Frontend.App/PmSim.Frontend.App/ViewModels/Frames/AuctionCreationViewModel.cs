using System.Collections.ObjectModel;
using System.Reactive;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;
using PmSim.Shared.Contracts.Interfaces;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class AuctionCreationViewModel : BasicFrameViewModel
{
    private int _initialPrice = 1;

    public int InitialPrice
    {
        get => _initialPrice;
        set => this.RaiseAndSetIfChanged(ref _initialPrice, value);
    }
    
    private IAuctionLot? _selectedLot;

    public IAuctionLot? SelectedLot
    {
        get => _selectedLot;
        set => this.RaiseAndSetIfChanged(ref _selectedLot, value);
    }

    public ObservableCollection<EmployeeStatus> Employees { get; } = new();
    
    public ObservableCollection<Project> Projects { get; } = new();
    
    public ObservableCollection<OpportunityModel> Opportunities { get; } = new();
    
    public ReactiveCommand<Unit, Unit> ConfirmCommand { get; }
    
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
    
    public AuctionCreationViewModel(GameScreenViewModel gameScreen) : base(gameScreen)
    {
        /*
        Employees.Add(new EmployeeStatus() { Name = "Oleg", Salary = 10, Skills = new SkillsPoints(1, 1, 1, 1, 1), TaskTypeName = "Работает"});
        var p0 = GameConstants.Projects[0];
        var p1 = GameConstants.Projects[1];
        var p2 = GameConstants.Projects[2];
        p0.Name = "Социальная сеть";
        p1.Name = "Мессенджер";
        p2.Name = "Интернет-магазин";
        Projects.Add(p0);
        Projects.Add(p1);
        Projects.Add(p2);
        var o = new OpportunityModel
        {
            Name = "Развод сотрудника",
            Description = "Брак вашего сотрудника внезапно распался. Вы любезно предложили пожить в офисе, попутно решив пару рабочих задач."
        };
        Opportunities.Add(o);
        //*/
        
        ConfirmCommand = ReactiveCommand.Create(ProcessConfirmation);
        CancelCommand = ReactiveCommand.Create(ProcessCancel);
    }
    
    private void ProcessConfirmation()
    {
        
    }
    
    private void ProcessCancel()
    {
        GameScreen.ShowMapMenu();
        GameScreen.CurrentTabIndex = 4;
    }
}