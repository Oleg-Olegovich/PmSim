using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game.Projects;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class ProjectsMenuViewModel : BasicFrameViewModel
{
    public List<Project> Projects { get; } = new();
    
    public ObservableCollection<Project> CurrentProjects { get; } = new();
    
    public ObservableCollection<Project> Ideas { get; } = new();
    
    public ObservableCollection<Project> CompletedProjects { get; } = new();
    
    public ObservableCollection<Project> FailedProjects { get; } = new();
    
    public ReactiveCommand<Unit, Unit> StartDevelopmentCommand { get; }

    private Project? _selectedIdea;
    public Project? SelectedIdea
    {
        get => _selectedIdea;
        set => this.RaiseAndSetIfChanged(ref _selectedIdea, value);
    }
    
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
        
        StartDevelopmentCommand = ReactiveCommand.Create(ProcessStartDevelopment);
    }

    private void ProcessStartDevelopment()
    {
        if (SelectedIdea is null)
        {
            return;
        }
        
        var id = Projects.IndexOf(SelectedIdea);
        if (id != -1)
        {
            GameScreen.RequestStartProject(id);
        }
    }
}