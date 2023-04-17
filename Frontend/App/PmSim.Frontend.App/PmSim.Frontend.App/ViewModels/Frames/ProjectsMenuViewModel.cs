using System.Collections.ObjectModel;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Projects;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class ProjectsMenuViewModel : BasicFrameViewModel
{
    public ObservableCollection<Project> CurrentProjects { get; } = new();
    
    public ObservableCollection<Project> Ideas { get; } = new();
    
    public ObservableCollection<Project> CompletedProjects { get; } = new();
    
    public ObservableCollection<Project> FailedProjects { get; } = new();
    
    public ProjectsMenuViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen)
    {
        /*
        var p0 = GameConstants.Projects[0];
        var p1 = GameConstants.Projects[1];
        var p2 = GameConstants.Projects[2];
        p0.Name = "Социальная сеть";
        p1.Name = "Мессенджер";
        p2.Name = "Интернет-магазин";
        CurrentProjects.Add(p0);
        CurrentProjects.Add(p1);
        CurrentProjects.Add(p2);
        //*/
    }
}