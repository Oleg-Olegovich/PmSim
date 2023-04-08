namespace PmSim.Shared.Contracts.Game.Projects;

public class ProjectBackUp : FeatureBackUp
{
    public FeatureBackUp[] FeatureBackUps { get; }

    public ProjectBackUp(ProgressPoints progressPoints, Feature[] features)
        : base(progressPoints)
        => FeatureBackUps = GetBackUps(features);
    
    public ProjectBackUp(Project project)
        : this(project.Points, project.Features)
    { }
}