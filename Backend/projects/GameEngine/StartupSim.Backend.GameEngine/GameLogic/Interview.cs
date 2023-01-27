using System;
using StartupSim.Backend.GameEngine.Interfaces;

namespace StartupSim.Backend.GameEngine.GameLogic
{

    public class Interview : IPlayerAction
    {
        public int PlayerId { get; }

        public Employee Employee { get; } = Employee.Generate();
        
        public Interview(int playerId)
        {
            PlayerId = playerId;
        }
        
        public bool Process(int proposedSalary)
        {
            var random = new Random();
            var result = random.NextDouble();
            if (result > (double) proposedSalary / Employee.DesiredSalary)
            {
                return false;
            }

            Employee.Salary = proposedSalary;
            return true;
        }
    }
}