using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using PmSim.Backend.GameEngine.Dto;
using PmSim.Backend.GameEngine.Exceptions;
using PmSim.Backend.GameEngine.GameLogic.GameMaps;
using PmSim.Backend.GameEngine.GameLogic.Incidents;
using PmSim.Backend.GameEngine.GameLogic.Opportunities;
using PmSim.Backend.Gateway.Contracts.Enums;

namespace PmSim.Backend.GameEngine.GameLogic
{
    public class Game
    {
        private int _playersQuantity;

        /// <summary>
        /// The field that is universal for different stages of the game,
        /// storing the number of players who have completed the current activity.
        /// </summary>
        private int _playersCompleted;

        private readonly GameOptions _settings;
        private readonly List<Player> _players = new List<Player>();
        private readonly List<Auction> _auctions = new List<Auction>();
        private Bot[] _bots;
        private Incident[] _incidents;
        private Opportunity[] _opportunities;

        private readonly List<Interview> _interviews = new List<Interview>();
        private readonly IGameMap _map;
        private readonly Random _random = new Random();

        public GameStages Stage { get; private set; } = GameStages.NotStarted;

        /// <summary>
        /// Seconds of the current stage of the game.
        /// </summary>
        public int Time { get; private set; }

        public Incident CurrentIncident { get; private set; }

        public string Founder { get; }

        public int Id { get; }

        public Office[] Offices => _map.Offices;

        public Player[] Actors
        {
            get
            {
                var actors = new List<Player>(_players);
                actors.AddRange(_bots);
                return actors.ToArray();
            }
        }

        public Player Winner { get; private set; }

        public int GameMap => _map.MapImageNumber;

        public Game(string founder, int id, int players, int bots, GameOptions settings)
        {
            _map = FindMap(settings.MapNumber);
            Founder = founder;
            Id = id;
            _settings = settings;
            InitializeGameObjects();
            CreateActors(players, bots);
            Task.Run(ProcessAsync);
        }

        public async Task ConnectAsync(int id, string playerName)
        {
            if (Stage != GameStages.Connection && Stage != GameStages.NotStarted)
            {
                throw new WrongGameStageException("Now is not the connection.");
            }

            if (_players.Count == _playersQuantity)
            {
                throw new InvalidActionException("The limit on the quantity of players has been reached.");
            }

            _players.Add(new Player(id, playerName, _settings.StartUpCapital));
        }

        public Player FindPlayerById(int playerId)
        {
            var player = Actors.FirstOrDefault(x => x.Id == playerId);
            if (player == null)
            {
                throw new InvalidActionException("There is no such player.");
            }

            return player;
        }

        public async Task ChooseBackgroundAsync(int playerId, Professions profession)
        {
            var player = FindPlayerById(playerId);
            if (player.IsBackgroundChosen)
            {
                throw new InvalidActionException("The background has already been chosen.");
            }

            ++_playersCompleted;
            player.IsBackgroundChosen = true;
            player.SetSkillsByProfession(profession);
        }

        public async Task RentOfficeAsync(int playerId, int officeNumber)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != -1 || player.Money < Offices[officeNumber].RentalPrice)
            {
                throw new InvalidActionException("It is impossible to rent the office.");
            }

            Offices[officeNumber].OwnerId = playerId;
            player.Money -= Offices[officeNumber].RentalPrice;
            if (!player.IsStartupOpen)
            {
                player.IsStartupOpen = true;
                Offices[officeNumber].AddEmployee(player);
            }
        }

        public async Task CancelOfficeLeaseAsync(int playerId, int officeNumber)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || Offices.Count(x => x.OwnerId == playerId) < 2 || officeNumber < 0
                || officeNumber >= Offices.Length || Offices[officeNumber].OwnerId != playerId)
            {
                throw new InvalidActionException("It is impossible to cancel the office lease.");
            }

            Offices[officeNumber].Employees.Clear();
            Offices[officeNumber].OwnerId = -1;
        }

        public async Task DismissAllEmployeesAsync(int playerId, int officeNumber)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != playerId)
            {
                throw new InvalidActionException("It is impossible to dismiss all employees.");
            }

            Offices[officeNumber].Employees.Clear();
        }

        public async Task<Employee> ConductInterviewAsync(int playerId, int officeNumber)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != playerId
                || Offices[officeNumber].Employees.Count == Offices[officeNumber].Capacity)
            {
                throw new InvalidActionException("It is impossible to conduct the interview.");
            }

            if (player.ActionsNumber == 0)
            {
                throw new InvalidActionException("The limit of actions has been reached.");
            }

            --player.ActionsNumber;
            var interview = new Interview(playerId);
            _interviews.Add(interview);
            return interview.Employee;
        }

        public async Task<bool> ProcessInterviewAsync(int playerId, int officeNumber, int proposedSalary)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || player.Money < proposedSalary || proposedSalary < 1 || officeNumber < 0
                || officeNumber >= Offices.Length || Offices[officeNumber].OwnerId != playerId
                || Offices[officeNumber].Employees.Count == Offices[officeNumber].Capacity)
            {
                throw new InvalidActionException("It is impossible to process the interview.");
            }

            var interview = FindInterviewById(playerId);
            var result = interview.Process(proposedSalary);
            if (result)
            {
                Offices[officeNumber].AddEmployee(interview.Employee);
            }

            _interviews.Remove(interview);
            return result;
        }

        public async Task HireTechSupportOfficerAsync(int playerId, int officeNumber)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != playerId || Offices[officeNumber].DoesHaveTechSupport)
            {
                throw new InvalidActionException("It is impossible to hire the tech support officer.");
            }

            Offices[officeNumber].DoesHaveTechSupport = true;
        }

        public async Task DismissTechSupportOfficerAsync(int playerId, int officeNumber)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != playerId || !Offices[officeNumber].DoesHaveTechSupport)
            {
                throw new InvalidActionException("It is impossible to dismiss the tech support officer.");
            }

            Offices[officeNumber].DoesHaveTechSupport = false;
        }

        public async Task UseOpportunityAsync(int playerId, int opportunityNumber, int targetPlayer)
        {
            var player = FindPlayerById(playerId);
            var target = FindPlayerById(targetPlayer);
            if (player.IsOut || target.IsOut || opportunityNumber < 0 || opportunityNumber >= _opportunities.Length
                || !player.Opportunities.Contains(opportunityNumber))
            {
                throw new InvalidActionException("It is impossible to use the opportunity.");
            }

            if (player.ActionsNumber == 0)
            {
                throw new InvalidActionException("The limit of actions has been reached.");
            }

            --player.ActionsNumber;
            _opportunities[opportunityNumber].Process(target);
            player.Opportunities.Remove(opportunityNumber);
        }

        public async Task AssignToWorkAsync(int playerId, int officeNumber, int executorNumber,
            int featureNumber, int progressPointNumber)
        {
            if (progressPointNumber < 0 || progressPointNumber > 3)
            {
                throw new InvalidActionException("There is no such progress point.");
            }

            var player = FindPlayerById(playerId);
            if (featureNumber < 0 || featureNumber >= player.Projects.Count)
            {
                throw new InvalidActionException("There is no such feature.");
            }

            await AssignToTaskAsync(playerId, officeNumber, executorNumber, new EmployeeWorkTask(player,
                player.Projects[featureNumber], progressPointNumber));
        }

        public async Task AssignToInventProjectAsync(int playerId, int officeNumber, int executorNumber)
        {
            await AssignToTaskAsync(playerId, officeNumber, executorNumber, new EmployeeTask(FindPlayerById(playerId)));
        }

        public async Task AssignToMakeBackupAsync(int playerId, int officeNumber, int executorNumber, int projectNumber)
        {
            var player = FindPlayerById(playerId);
            if (projectNumber < 0 || projectNumber >= player.Projects.Count)
            {
                throw new InvalidActionException("There is no such feature.");
            }

            await AssignToTaskAsync(playerId, officeNumber, executorNumber,
                new EmployeeBackUpTask(player, player.Projects[projectNumber],
                    officeNumber));
        }

        public async Task CancelTaskAsync(int playerId, int officeNumber, int executorNumber)
        {
            if (FindPlayerById(playerId).IsOut || officeNumber < 0 || officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != playerId || executorNumber < 0
                || executorNumber >= Offices[officeNumber].Employees.Count)
            {
                throw new InvalidActionException("It is impossible to cancel the task.");
            }

            Offices[officeNumber].Employees[executorNumber].CurrentTask = null;
        }

        public async Task ProposeProjectAsync(int sellerId, int projectNumber, int startPrice, int buyerId)
        {
            var seller = FindPlayerById(sellerId);
            if (seller.IsOut || buyerId != -1 && FindPlayerById(buyerId).IsOut || projectNumber < 0
                || projectNumber >= _opportunities.Length
                || _auctions.Count(x => x.Lot == seller.Projects[projectNumber]) > 0)
            {
                throw new InvalidActionException("It is impossible to put the project up for auction.");
            }

            _auctions.Add(new Auction(_auctions.Count, seller.Projects[projectNumber], sellerId, startPrice, buyerId));
        }

        public async Task PutProjectUpForAuctionAsync(int playerId, int projectNumber, int startPrice)
            => await ProposeProjectAsync(playerId, projectNumber, startPrice, -1);

        public async Task ProposeExecutorAsync(int sellerId, int officeNumber, int executorNumber, int startPrice,
            int buyerId)
        {
            if (FindPlayerById(sellerId).IsOut || buyerId != -1 && FindPlayerById(buyerId).IsOut || officeNumber < 0 ||
                officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != sellerId || executorNumber < 0
                || executorNumber >= Offices[officeNumber].Employees.Count
                || _auctions.Count(x => x.Lot == Offices[officeNumber].Employees[executorNumber]) > 0)
            {
                throw new InvalidActionException("It is impossible to put the executor up for auction.");
            }

            _auctions.Add(new Auction(_auctions.Count, Offices[officeNumber].Employees[executorNumber], sellerId,
                startPrice, buyerId));
        }

        public async Task PutExecutorUpForAuctionAsync(int playerId, int officeNumber, int executorNumber,
            int startPrice)
            => await ProposeExecutorAsync(playerId, officeNumber, executorNumber, startPrice, -1);

        public async Task ProposeOpportunityAsync(int sellerId, int opportunityNumber, int startPrice, int buyerId)
        {
            var seller = FindPlayerById(sellerId);
            if (seller.IsOut || opportunityNumber < 0 || opportunityNumber >= _opportunities.Length
                || !seller.Opportunities.Contains(opportunityNumber))
            {
                throw new InvalidActionException("It is impossible to put the opportunity up for auction.");
            }

            _auctions.Add(
                new Auction(_auctions.Count, _opportunities[opportunityNumber], sellerId, startPrice, buyerId));
        }

        public async Task PutOpportunityUpForAuctionAsync(int playerId, int opportunityNumber, int startPrice)
            => await ProposeOpportunityAsync(playerId, opportunityNumber, startPrice, -1);

        /// <summary>
        /// The universal method for public and personal auctions.
        /// If the auction is public, then the seller does not participate, the bid can only increase.
        /// Otherwise, the seller can participate and raise the bid, the buyer can lower it.
        /// </summary>
        public async Task ParticipateInAuctionAsync(int buyerId, int auctionNumber, int offerSum)
        {
            var buyer = FindPlayerById(buyerId);
            if (buyer.IsOut || auctionNumber < 0 || auctionNumber >= _auctions.Count
                || !_auctions[auctionNumber].IsPublic && _auctions[auctionNumber].LastBuyerId != buyerId
                || !_auctions[auctionNumber].IsPublic && _auctions[auctionNumber].LastBuyerId == buyerId
                                                      && offerSum >= _auctions[auctionNumber].LastPrice ||
                _auctions[auctionNumber].IsPublic
                && _auctions[auctionNumber].SellerId == buyerId || (_auctions[auctionNumber].IsPublic
                                                                    || _auctions[auctionNumber].SellerId == buyerId) &&
                _auctions[auctionNumber].LastPrice >= offerSum
                || buyer.Money < offerSum)
            {
                throw new InvalidActionException("It is impossible to participate in the auction.");
            }

            if (_auctions[auctionNumber].LastBuyerId != -1)
            {
                var lastBuyer = FindPlayerById(_auctions[auctionNumber].LastBuyerId);
                lastBuyer.Money += _auctions[auctionNumber].LastPrice;
            }

            if (_auctions[auctionNumber].IsPublic)
            {
                _auctions[auctionNumber].LastBuyerId = buyerId;
            }
            else if (_auctions[auctionNumber].SellerId != buyerId)
            {
                _auctions[auctionNumber].Accepted = true;
            }

            _auctions[auctionNumber].LastPrice = offerSum;
            buyer.Money -= offerSum;
        }

        /// <summary>
        /// Returns a list of public auctions, personal offers for this player and offers made by this player.
        /// </summary>
        public async Task<Auction[]> RequestIncomingOffersAsync(int playerId)
        {
            if (FindPlayerById(playerId).IsOut || Stage != GameStages.Diplomacy)
            {
                throw new InvalidActionException("It is impossible to request incoming offers.");
            }

            return _auctions.Where(x => x.IsPublic || x.SellerId == playerId || x.LastBuyerId == playerId).ToArray();
        }

        public async Task MakeDecisionOnIncidentAsync(int playerId, int donation)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || player.Money < donation)
            {
                throw new InvalidActionException("It is impossible to make decision on the incident.");
            }

            CurrentIncident.DonationsSum += donation;
            player.Money -= donation;
        }

        public async Task SkipMoveAsync(int playerId)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut)
            {
                throw new InvalidActionException("It is impossible to skip move.");
            }

            player.ActionsNumber = 0;
        }

        public async Task GiveUpAsync(int playerId)
        {
            var player = FindPlayerById(playerId);
            player.IsOut = true;
        }

        /// <summary>
        /// This feature will be added in the release or multiplayer version.
        /// It may be worth moving the method to Gateway.
        /// </summary>
        public async Task SendMessageAsync(int playerId, int otherPlayerId, string message)
        {
            var player = FindPlayerById(playerId);
        }

        /// <summary>
        /// This feature will be added in the release or multiplayer version.
        /// It may be worth moving the method to Gateway.
        /// </summary>
        public async Task SendMessageToEveryoneAsync(int playerId, string message)
        {
            var player = FindPlayerById(playerId);
        }

        public static Project GetProjectRandomly()
            => (Project) GameConstants.Projects[(new Random()).Next(GameConstants.Projects.Length)].Clone();

        private async Task AssignToTaskAsync(int playerId, int officeNumber, int executorNumber, EmployeeTask task)
        {
            var player = FindPlayerById(playerId);
            if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
                || Offices[officeNumber].OwnerId != playerId || executorNumber < 0
                || executorNumber >= Offices[officeNumber].Employees.Count)
            {
                throw new InvalidActionException("It is impossible to assign to work.");
            }

            Offices[officeNumber].Employees[executorNumber].CurrentTask = task;
        }
        
        private void InitializeIncidents()
            => _incidents = GetInheritsInstances<Incident>();

        private void InitializeOpportunities()
            => _opportunities = GetInheritsInstances<Opportunity>(typeof(Incident));

        private void InitializeGameObjects()
        {
            try
            {
                InitializeIncidents();
                InitializeOpportunities();
            }
            catch
            {
                throw new GameLogicException("Failed to initialize game objects.");
            }
        }

        private void CreateActors(int playersQuantity, int botsQuantity)
        {
            if (playersQuantity < 0)
            {
                throw new ArgumentException("Incorrect number of players.");
            }

            if ((playersQuantity + botsQuantity < 2) ||
                (playersQuantity + botsQuantity > GameConstants.MaxActorsNumber))
            {
                throw new ArgumentException("Incorrect number of bots or players.");
            }

            _playersQuantity = playersQuantity;

            _bots = new Bot[botsQuantity];
            for (var i = 0; i < botsQuantity; ++i)
            {
                _bots[i] = new Bot(playersQuantity + i, $"Bot {i + 1}", _settings.StartUpCapital, this);
            }
        }

        private async Task ProcessConnectionAsync()
        {
            Stage = GameStages.Connection;
            Time = _settings.ConnectionRealTime;
            while (Time > 0 && _players.Count < _playersQuantity)
            {
                await Task.Delay(1000);
                --Time;
            }

            _playersQuantity = _players.Count;
        }

        private async Task ProcessGameStageAsync(GameStages stage, int time)
        {
            _playersCompleted = 0;
            Stage = stage;
            Time = time;
            while (Time > 0 && _playersCompleted < _playersQuantity)
            {
                await Task.Delay(1000);
                --Time;
            }
        }

        private async Task ProcessChoosingBackgroundAsync()
            => await ProcessGameStageAsync(GameStages.ChoosingBackground, _settings.ChoosingBackgroundRealTime);

        private async Task ProcessSprintAsync()
            => await ProcessGameStageAsync(GameStages.Sprint, _settings.SprintRealTime);

        private async Task ProcessDiplomacyAsync()
        {
            _playersCompleted = 0;
            Stage = GameStages.Diplomacy;
            Time = _settings.DiplomacyRealTime;
            while (Time > 0 && _playersCompleted < _playersQuantity)
            {
                var tasks = _bots.Select(bot => bot.MakeDiplomaticActions()).ToList();
                foreach (var task in tasks)
                {
                    await task;
                }

                await Task.Delay(1000);
                --Time;
            }
        }

        private async Task SummingUpExpenses()
        {
            foreach (var office in Offices)
            {
                if (office.OwnerId == -1)
                {
                    continue;
                }

                var owner = FindPlayerById(office.OwnerId);
                if (office.DoesHaveTechSupport)
                {
                    owner.Money -= GameConstants.TechSupportSalary;
                }

                foreach (var employee in office.Employees)
                {
                    owner.Money -= employee.Salary;
                }
            }
        }

        private async Task SummingUpAuctions()
        {
            foreach (var auction in _auctions)
            {
                if (auction.LastBuyerId == -1 || !auction.IsPublic && !auction.Accepted)
                {
                    continue;
                }

                var seller = FindPlayerById(auction.SellerId);
                var buyer = FindPlayerById(auction.LastBuyerId);
                buyer.Money -= auction.LastPrice;
                seller.Money += auction.LastPrice;

                switch (auction.Lot)
                {
                    case Project project:
                        buyer.Projects.Add(project);
                        seller.Projects.Remove(project);
                        continue;
                    case Opportunity opportunity:
                        buyer.Opportunities.Add(opportunity.DescriptionNumber);
                        seller.Opportunities.Remove(opportunity.DescriptionNumber);
                        continue;
                    case Employee employee:
                        // This feature will be added in the release or multiplayer version.
                        break;
                    default:
                        throw new GameLogicException("Invalid auction lot!");
                }
            }
        }

        private async Task SummingUpEmployeesTasks()
        {
            foreach (var office in Offices)
            {
                if (office.OwnerId == -1)
                {
                    continue;
                }

                foreach (var employee in office.Employees)
                {
                    employee.Work();
                }
            }
        }

        private async Task ProcessSummingUpAsync()
        {
            Stage = GameStages.SummingUpResults;

            await SummingUpExpenses();
            await SummingUpAuctions();
            await SummingUpEmployeesTasks();

            var tasks = Actors.Select(player => player.SummingUp()).ToList();
            foreach (var task in tasks)
            {
                await task;
            }
        }

        private async Task ProcessIncidentAsync()
        {
            CurrentIncident = _incidents[_random.Next(_incidents.Length)];
            await ProcessGameStageAsync(GameStages.IncidentDiscussion, _settings.IncidentRealTime);
            var tasks = _bots.Select(bot => bot.MakeIncidentDecision()).ToList();
            foreach (var task in tasks)
            {
                await task;
            }

            Stage = GameStages.IncidentResolution;
            if (CurrentIncident.DonationsSum < CurrentIncident.PassCost)
            {
                foreach (var player in _players)
                {
                    CurrentIncident.Process(player);
                }

                foreach (var bot in _bots)
                {
                    CurrentIncident.Process(bot);
                }
            }
        }

        private async Task MakeBotSprintMovesAsync()
        {
            var tasks = (from bot in _bots
                where bot.ActionsNumber > 0
                select bot.MakeSprintActions()).ToArray();
            foreach (var task in tasks)
            {
                await task;
            }
        }

        private async Task ApproveWinnerAsync(Func<Player, bool> condition)
        {
            Winner = _players.FirstOrDefault(condition);
            if (Winner != null)
            {
                return;
            }

            Winner = _bots.FirstOrDefault(condition);
            if (Winner == null)
            {
                throw new GameLogicException("There is no winner.");
            }
        }

        private async Task ApproveWinnerAsync()
        {
            switch (_settings.Mode)
            {
                case GameModes.Survival:
                    await ApproveWinnerAsync((player => !player.IsOut));
                    break;
                case GameModes.TimerAndMoney:
                    var max = Math.Max(_players.Max(player => player.Money),
                        _bots.Max(bot => bot.Money));
                    await ApproveWinnerAsync((player => player.Money == max));
                    break;
                case GameModes.TimerAndProjects:
                    var projects = Math.Max(_players.Max(player => player.CompletedProjects),
                        _bots.Max(bot => bot.CompletedProjects));
                    await ApproveWinnerAsync((player => player.CompletedProjects == projects));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private Interview FindInterviewById(int playerId)
        {
            var interview = _interviews.FirstOrDefault(x => x.PlayerId == playerId);
            if (interview == null)
            {
                throw new InvalidActionException("There is no such player.");
            }

            return interview;
        }

        private async Task ProcessAsync()
        {
            await ProcessConnectionAsync();
            await ProcessChoosingBackgroundAsync();
            while (Stage != GameStages.IsOver)
            {
                foreach (var actor in Actors)
                {
                    actor.ActionsNumber = _settings.SprintActionsNumbers;
                    actor.Projects.Add(GetProjectRandomly());
                }

                await ProcessSprintAsync();
                await MakeBotSprintMovesAsync();
                await ProcessDiplomacyAsync();
                await ProcessSummingUpAsync();
                await ProcessIncidentAsync();
            }

            await ApproveWinnerAsync();
        }

        private static IGameMap FindMap(int number)
        {
            try
            {
                var handler = Activator.CreateInstance(null,
                    "PmSim.Backend.GameEngine.GameLogic.GameMaps.Map" + number);
                return (IGameMap) handler.Unwrap();
            }
            catch
            {
                throw new InvalidMapNumberException("Invalid map number!");
            }
        }

        private static T[] GetInheritsInstances<T>(Type forbidden = null)
        {
            var parent = typeof(T);
            var inherits = Assembly.GetAssembly(parent).GetTypes()
                .Where(type => type.IsSubclassOf(parent) 
                               && (forbidden == null || !type.IsSubclassOf(forbidden) && type != forbidden))
                .ToArray();
            var instances = new T[inherits.Count()];
            for (var i = 0; i < instances.Length; ++i)
            {
                instances[i] = (T) Activator.CreateInstance(inherits[i]);
            }

            return instances;
        }
    }
}