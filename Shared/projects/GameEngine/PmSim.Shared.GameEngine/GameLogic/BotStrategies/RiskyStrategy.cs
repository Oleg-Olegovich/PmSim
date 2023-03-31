using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;

namespace PmSim.Shared.GameEngine.GameLogic.BotStrategies
{
    internal class RiskyStrategy : BotStrategy
    {
        internal RiskyStrategy(Game game, Bot bot) 
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
                    Game.Offices[i].AddEmployee(Bot);
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
            var auctions = Game.RequestIncomingOffers(Bot.Id);
            foreach (var auction in auctions)
            {
                if (Random.Next(100) < 50 || !auction.IsPublic || auction.LastBuyerId == Bot.Id 
                    || auction.LastPrice >= Bot.Money)
                {
                    continue;
                }

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
                while (Game.Offices[index].Employees.Count < Game.Offices[index].Capacity && Bot.ActionsNumber > 0
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
            foreach (var employee in Bot.OfficeIndexes.SelectMany(index => Game.Offices[index].Employees))
            {
                employee.CurrentTask = new EmployeeWorkTask(Bot, Bot.Projects[Random.Next(Bot.Projects.Count)],
                    Random.Next(4));
            }
        }
    }
}