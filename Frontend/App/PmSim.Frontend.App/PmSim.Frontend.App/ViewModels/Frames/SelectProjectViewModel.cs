using System.Collections.ObjectModel;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game.Projects;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class SelectProjectViewModel : BasicFrameViewModel
{
    public ObservableCollection<Project> CurrentProjects { get; }
    
    private Project? _selectedProject;
    public Project? SelectedProject
    {
        get => _selectedProject;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedProject, value);
            GameScreen.ShowMapMenu();
            GameScreen.CurrentTabIndex = 1;
        }
    }

    public SelectProjectViewModel(GameScreenViewModel gameScreen, ObservableCollection<Project> projects) 
        : base(gameScreen) 
        => CurrentProjects = projects;
}