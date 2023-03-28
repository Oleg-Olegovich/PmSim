namespace PmSim.Shared.GameEngine.Dto
{
    internal static class GameConstants
    {
        internal static Project[] Projects { get; }

        private static Feature[] Features { get; }
        
        static GameConstants()
        {
            Features = new []{
                new Feature(0, new ProgressPoints(3, 1, 1, 2), new Reward(0, 1, 0)),
                new Feature(1, new ProgressPoints(3, 0, 1, 2), new Reward(0, 1, 0)),
                new Feature(2, new ProgressPoints(5, 1, 2, 3), new Reward(0, 1, 1)),
            };
            Projects = new []{
                new Project(0, new ProgressPoints(10, 9, 0, 6), new Reward(0, 40, 1), 4, new[]
                {
                    Features[0].Clone() as Feature,
                }),
                new Project(1, new ProgressPoints(6, 3, 0, 4), new Reward(30, 0, 0), 3, new[]
                {
                    Features[1].Clone() as Feature,
                }),
                new Project(2, new ProgressPoints(13, 11, 0, 10), new Reward(0, 40, 1), 4, new[]
                {
                    Features[2].Clone() as Feature,
                }),
                new Project(3, new ProgressPoints(15, 7, 0, 6), new Reward(0, 30, 0), 4, null),
                new Project(4, new ProgressPoints(10, 6, 0, 4), new Reward(0, 20, 0), 3, null),
                new Project(5, new ProgressPoints(11, 4, 0, 6), new Reward(0, 15, 1), 2, null),
                new Project(6, new ProgressPoints(9, 6, 0, 4), new Reward(0, 25, 1), 2, null),
                new Project(7, new ProgressPoints(6, 3, 0, 3), new Reward(30, 0, 1), 2, null),
                new Project(8, new ProgressPoints(4, 3, 10, 3), new Reward(30, 0, 0), 2, null),
                new Project(9, new ProgressPoints(4, 3, 0, 3), new Reward(30, 0, 0), 2, null),
                new Project(10, new ProgressPoints(20, 22, 10, 8), new Reward(100, 0, 0), 5, null),
                new Project(11, new ProgressPoints(20, 22, 10, 8), new Reward(100, 0, 0), 5, null),
                new Project(12, new ProgressPoints(15, 13, 10, 7), new Reward(70, 0, 0), 4, null),
                new Project(13, new ProgressPoints(5, 7, 0, 3), new Reward(0, 15, 0), 2, null),
                new Project(14, new ProgressPoints(20, 10, 0, 6), new Reward(50, 0, 0), 4, null),
                new Project(15, new ProgressPoints(15, 7, 0, 3), new Reward(34, 0, 0), 3, null),
                new Project(16, new ProgressPoints(10, 3, 0, 3), new Reward(20, 0, 0), 2, null),
                new Project(17, new ProgressPoints(10, 6, 0, 6), new Reward(20, 0, 0), 3, null),
                new Project(18, new ProgressPoints(100, 0, 0, 15), new Reward(200, 50, 5), 8, null),
            };
        }
    }
}