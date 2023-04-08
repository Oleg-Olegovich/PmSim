using System;
using System.Linq;
using System.Threading.Tasks;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Employees;


namespace PmSim.Shared.GameEngine.GameLogic.BotStrategies
{
    internal class RandomStrategy : BotStrategy
    {
        internal RandomStrategy(Game game, Bot bot)
            : base(game, bot)
        {
        }

        internal override void WorkForHire()
        {
            var tryRent = Random.Next(100) < 50;
            if (tryRent)
            {
                for (var i = 0; i < Game.Offices.Length; ++i)
                {
                    if (Game.Offices[i].RentalPrice > Bot.Money)
                    {
                        continue;
                    }

                    Game.Offices[i].OwnerId = Bot.Id;
                    Bot.OfficeIndexes.Add(i);
                    Bot.Money -= Game.Offices[i].RentalPrice;
                    Bot.IsStartupOpen = true;
                }

                return;
            }

            Bot.Money += GameConstants.WorkForHireSalary;
        }

        internal override void MakeSprintMove()
        {
            ConductInterviews();
            AssignToWork();
        }

        internal override void MakeDiplomaticMove()
        {
            foreach (var auction in Game.Auctions.Where(
                         auction => Random.Next(100) >= 50 && auction.LastPrice < Bot.Money))
            {
                Game.ParticipateInAuction(Bot.Id, auction.Id, auction.LastPrice + 1);
            }
        }

        internal override void MakeIncidentDecision()
        {
            if (Bot.Money == 0)
            {
                return;
            }

            Game.MakeDecisionOnIncident(Bot.Id, 1);
        }

        private void ConductInterviews()
        {
            var money = Bot.Money;
            foreach (var index in Bot.OfficeIndexes)
            {
                while (Bot.Employees.Count < Bot.MaxEmployeesNumber && Bot.ActionsNumber > 0
                       && money > 0)
                {
                    --Bot.ActionsNumber;
                    var interview = new Interview(Bot.Id);
                    if (interview.Process(Math.Min(interview.Employee.DesiredSalary, money)))
                    {
                        money -= interview.Employee.Salary;
                    }
                }

                if (Bot.ActionsNumber == 0 || money == 0)
                {
                    break;
                }
            }
        }

        private void AssignToWork()
        {
            foreach (var employee in Bot.OfficeIndexes.SelectMany(index => Bot.Employees))
            {
                employee.CurrentTask = new EmployeeDevelopmentTask(Bot, Bot.Projects[Random.Next(Bot.Projects.Count)],
                    Random.Next(4));
            }
        }
    }
}