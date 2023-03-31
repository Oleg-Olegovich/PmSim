using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.GameEngine.GameLogic
{

    internal class Interview : IPlayerAction
    {
        internal int PlayerId { get; }

        internal Employee Employee { get; } = EmployeeLogic.Generate();
        
        internal Interview(int playerId)
        {
            PlayerId = playerId;
        }
        
        internal bool Process(int proposedSalary)
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