using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.Projects;

public class Project : Feature, IAuctionLot
{
    public Feature[] Features { get; }

    private int _deadline;
    public int Deadline
    {
        get => _deadline;
        set => _deadline = value < 0 ?  0 : value;
    }

    public bool IsFailed
        => Deadline == 0 && !IsDone;

    public ProjectBackUp LastBackUp { get; set; }
    
    public Project(int nameNumber, ProgressPoints points, Reward reward, int deadline, Feature[]? features = null)
        : base(nameNumber, points, reward)
    {
        _deadline = deadline;
        Features = features ?? Array.Empty<Feature>();
        LastBackUp = new ProjectBackUp(Points, Features);
    }

    public Project(Project project)
        : this(project.DescriptionNumber, new ProgressPoints(project.Points), new Reward(project.Reward),
            project.Deadline, Copy(project.Features))
    { }
    
    public void ResetToBackUp()
    {
        Points = new ProgressPoints(LastBackUp.Points);
        for (var i = 0; i < Features.Length; ++i)
        {
            Features[i].Points = new ProgressPoints(LastBackUp.FeatureBackUps[i].Points);
        }
    }
}