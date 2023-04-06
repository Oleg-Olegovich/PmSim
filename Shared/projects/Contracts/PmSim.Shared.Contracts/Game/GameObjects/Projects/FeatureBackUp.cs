namespace PmSim.Shared.Contracts.Game.GameObjects.Projects;

public class FeatureBackUp
{
    public ProgressPoints Points { get; }

    public FeatureBackUp(ProgressPoints points) 
        => Points = new ProgressPoints(points);

    public static FeatureBackUp[] GetBackUps(Feature[] features)
    {
        var backUps = new FeatureBackUp[features.Length];
        for (var i = 0; i < features.Length; ++i)
        {
            backUps[i] = new FeatureBackUp(features[i].Points);
        }

        return backUps;
    }
}