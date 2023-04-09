using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.Contracts.Game.Projects;

public class Feature : ITranslationObject
{
    public int DescriptionNumber { get; }
    
    public string? Name { get; set; }

    public ProgressPoints Points { get; set; }

    public Reward Reward { get; }
        
    public bool IsDone
        => Points.IsDone;

    public Feature(int nameNumber, ProgressPoints points, Reward reward)
    {
        DescriptionNumber = nameNumber;
        Points = points;
        Reward = reward;
    }
    
    public Feature(Feature feature)
        : this(feature.DescriptionNumber, 
            new ProgressPoints(feature.Points), new Reward(feature.Reward))
    { }

    protected static Feature[] Copy(Feature[] features)
    {
        var newFeatures = new Feature[features.Length];
        for (var i = 0; i < features.Length; ++i)
        {
            newFeatures[i] = new Feature(features[i]);
        }

        return newFeatures;
    }
}