using System;
using System.Linq;
using System.Threading.Tasks;
using PmSim.Backend.GameEngine.Dto;
using PmSim.Backend.GameEngine.Interfaces;

namespace PmSim.Backend.GameEngine.GameLogic.BotStrategies
{
    public class RiskyStrategy : BotStrategy
    {
        public RiskyStrategy(Game game, Bot bot) 
            : base(game, bot)
        {
        }
        
        public override async Task WorkForHire()
        {
            var tryRent = _random.Next(100) < 50;
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

        public override async Task MakeSprintMove()
        {
            await ConductInterviews();
            await AssignToWork();
        }

        public override async Task MakeDiplomaticMove()
        {
            var auctions = await Game.RequestIncomingOffersAsync(Bot.Id);
            foreach (var auction in auctions)
            {
                if (_random.Next(100) < 50 || !auction.IsPublic || auction.LastBuyerId == Bot.Id 
                    || auction.LastPrice >= Bot.Money)
                {
                    continue;
                }

                await Game.ParticipateInAuctionAsync(Bot.Id, auction.Id, auction.LastPrice + 1);
            }
        }

        public override async Task MakeIncidentDecision()
        {
            if (Bot.Money == 0)
            {
                return;
            }

            await Game.MakeDecisionOnIncidentAsync(Bot.Id, 1);
        }

        private async Task ConductInterviews()
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

        private async Task AssignToWork()
        {
            foreach (var employee in Bot.OfficeIndexes.SelectMany(index => Game.Offices[index].Employees))
            {
                employee.CurrentTask = new EmployeeWorkTask(Bot, Bot.Projects[_random.Next(Bot.Projects.Count)],
                    _random.Next(4));
            }
        }
    }
}