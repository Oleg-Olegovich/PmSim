using System.Reflection;
using PmSim.Shared.GameEngine.GameLogic;
using PmSim.Shared.GameEngine.GameLogic.Incidents;
using PmSim.Shared.GameEngine.GameLogic.Opportunities;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.GameObjects.Diplomacy;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Maps;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Shared.GameEngine;

public class Game
{
    private readonly Task _mainProcess;

    private int _playersQuantity;

    /// <summary>
    /// The field that is universal for different stages of the game,
    /// storing the number of players who have completed the current activity.
    /// </summary>
    private int _playersCompleted;

    private readonly GameOptions _settings;
    private readonly List<Player> _players = new();
    private readonly List<Auction> _auctions = new();
    private Bot[] _bots = Array.Empty<Bot>();
    private Incident[] _incidents = Array.Empty<Incident>();
    private Opportunity[] _opportunities = Array.Empty<Opportunity>();

    private readonly List<Interview> _interviews = new();
    private readonly IGameMap _map;
    private readonly Random _random = new();
    
    public Office[] Offices => _map.Offices;

    private GameStages _stage = GameStages.NotStarted;

    /// <summary>
    /// Seconds of the current stage of the game.
    /// </summary>
    private int Time { get; set; }

    private Incident? CurrentIncident { get; set; }

    private string Founder { get; }

    private int Id { get; }

    private Player[] Actors
    {
        get
        {
            var actors = new List<Player>(_players);
            actors.AddRange(_bots);
            return actors.ToArray();
        }
    }

    private Player? Winner { get; set; }

    private int GameMap => _map.MapImageNumber;

    public Game(string founder, int id, GameOptions settings)
    {
        _map = FindMap(settings.MapNumber);
        Founder = founder;
        Id = id;
        _settings = settings;
        InitializeGameObjects();
        CreateActors(settings.MaxPlayersNumber, settings.BotsNumber);
        _mainProcess = Task.Run(ProcessAsync);
    }

    /// <summary>
    /// It is recommended to call the method at the end of
    /// working with the game to complete the parallel task.
    /// </summary>
    public async Task StopGame()
        => await _mainProcess;

    public void Connect(int playerId, string playerName, IStatusChangeNotifier statusChangeNotifier)
    {
        if (_stage != GameStages.Connection && _stage != GameStages.NotStarted)
        {
            throw new PmSimException("Now is not the connection.");
        }

        if (_players.Count == _playersQuantity)
        {
            throw new PmSimException("The limit on the quantity of players has been reached.");
        }

        _players.Add(new Player(playerId, playerName, _settings.StartUpCapital, statusChangeNotifier));
    }

    public void SetBackground(int playerId, Professions profession)
    {
        var player = FindPlayerById(playerId);
        if (player.IsBackgroundChosen)
        {
            throw new PmSimException("The background has already been chosen.");
        }

        ++_playersCompleted;
        player.IsBackgroundChosen = true;
        PlayerLogic.SetSkillsByProfession(player, profession);
    }

    private void RentOffice(int playerId, int officeNumber)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != -1 || player.Money < Offices[officeNumber].RentalPrice)
        {
            throw new PmSimException("It is impossible to rent the office.");
        }

        Offices[officeNumber].OwnerId = playerId;
        player.Money -= Offices[officeNumber].RentalPrice;
        if (!player.IsStartupOpen)
        {
            player.IsStartupOpen = true;
            Offices[officeNumber].AddEmployee(player);
        }
    }

    private void CancelOfficeLease(int playerId, int officeNumber)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || Offices.Count(x => x.OwnerId == playerId) < 2 || officeNumber < 0
            || officeNumber >= Offices.Length || Offices[officeNumber].OwnerId != playerId)
        {
            throw new PmSimException("It is impossible to cancel the office lease.");
        }

        Offices[officeNumber].Employees.Clear();
        Offices[officeNumber].OwnerId = -1;
    }

    private void DismissAllEmployees(int playerId, int officeNumber)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != playerId)
        {
            throw new PmSimException("It is impossible to dismiss all employees.");
        }

        Offices[officeNumber].Employees.Clear();
    }

    private Employee ConductInterview(int playerId, int officeNumber)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != playerId
            || Offices[officeNumber].Employees.Count == Offices[officeNumber].Capacity)
        {
            throw new PmSimException("It is impossible to conduct the interview.");
        }

        if (player.ActionsNumber == 0)
        {
            throw new PmSimException("The limit of actions has been reached.");
        }

        --player.ActionsNumber;
        var interview = new Interview(playerId);
        _interviews.Add(interview);
        return interview.Employee;
    }

    private bool ProcessInterview(int playerId, int officeNumber, int proposedSalary)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || player.Money < proposedSalary || proposedSalary < 1 || officeNumber < 0
            || officeNumber >= Offices.Length || Offices[officeNumber].OwnerId != playerId
            || Offices[officeNumber].Employees.Count == Offices[officeNumber].Capacity)
        {
            throw new PmSimException("It is impossible to process the interview.");
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

    private void HireTechSupportOfficer(int playerId, int officeNumber)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != playerId || Offices[officeNumber].DoesHaveTechSupport)
        {
            throw new PmSimException("It is impossible to hire the tech support officer.");
        }

        Offices[officeNumber].DoesHaveTechSupport = true;
    }

    private void DismissTechSupportOfficer(int playerId, int officeNumber)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != playerId || !Offices[officeNumber].DoesHaveTechSupport)
        {
            throw new PmSimException("It is impossible to dismiss the tech support officer.");
        }

        Offices[officeNumber].DoesHaveTechSupport = false;
    }

    private void UseOpportunity(int playerId, int opportunityNumber, int targetPlayer)
    {
        var player = FindPlayerById(playerId);
        var target = FindPlayerById(targetPlayer);
        if (player.IsOut || target.IsOut || opportunityNumber < 0 || opportunityNumber >= _opportunities.Length
            || !player.Opportunities.Contains(opportunityNumber))
        {
            throw new PmSimException("It is impossible to use the opportunity.");
        }

        if (player.ActionsNumber == 0)
        {
            throw new PmSimException("The limit of actions has been reached.");
        }

        --player.ActionsNumber;
        _opportunities[opportunityNumber].Process(target);
        player.Opportunities.Remove(opportunityNumber);
    }

    private void AssignToWork(int playerId, int officeNumber, int executorNumber,
        int featureNumber, int progressPointNumber)
    {
        if (progressPointNumber < 0 || progressPointNumber > 3)
        {
            throw new PmSimException("There is no such progress point.");
        }

        var player = FindPlayerById(playerId);
        if (featureNumber < 0 || featureNumber >= player.Projects.Count)
        {
            throw new PmSimException("There is no such feature.");
        }

        AssignToTask(playerId, officeNumber, executorNumber, new EmployeeWorkTask(player,
            player.Projects[featureNumber], progressPointNumber));
    }

    private void AssignToInventProject(int playerId, int officeNumber, int executorNumber)
        => AssignToTask(playerId, officeNumber, executorNumber, new EmployeeTask(FindPlayerById(playerId)));

    private void AssignToMakeBackup(int playerId, int officeNumber, int executorNumber, int projectNumber)
    {
        var player = FindPlayerById(playerId);
        if (projectNumber < 0 || projectNumber >= player.Projects.Count)
        {
            throw new PmSimException("There is no such feature.");
        }

        AssignToTask(playerId, officeNumber, executorNumber,
            new EmployeeBackUpTask(player, player.Projects[projectNumber], officeNumber));
    }

    private void CancelTask(int playerId, int officeNumber, int executorNumber)
    {
        if (FindPlayerById(playerId).IsOut || officeNumber < 0 || officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != playerId || executorNumber < 0
            || executorNumber >= Offices[officeNumber].Employees.Count)
        {
            throw new PmSimException("It is impossible to cancel the task.");
        }

        Offices[officeNumber].Employees[executorNumber].CurrentTask = null;
    }

    private void ProposeProject(int sellerId, int projectNumber, int startPrice, int buyerId)
    {
        var seller = FindPlayerById(sellerId);
        if (seller.IsOut || buyerId != -1 && FindPlayerById(buyerId).IsOut || projectNumber < 0
            || projectNumber >= _opportunities.Length
            || _auctions.Count(x => x.Lot == seller.Projects[projectNumber]) > 0)
        {
            throw new PmSimException("It is impossible to put the project up for auction.");
        }

        _auctions.Add(new Auction(_auctions.Count, seller.Projects[projectNumber], sellerId, startPrice, buyerId));
    }

    private void PutProjectUpForAuction(int playerId, int projectNumber, int startPrice)
        => ProposeProject(playerId, projectNumber, startPrice, -1);

    private void ProposeExecutor(int sellerId, int officeNumber, int executorNumber, int startPrice,
        int buyerId)
    {
        if (FindPlayerById(sellerId).IsOut || buyerId != -1 && FindPlayerById(buyerId).IsOut || officeNumber < 0 ||
            officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != sellerId || executorNumber < 0
            || executorNumber >= Offices[officeNumber].Employees.Count
            || _auctions.Count(x => x.Lot == Offices[officeNumber].Employees[executorNumber]) > 0)
        {
            throw new PmSimException("It is impossible to put the executor up for auction.");
        }

        _auctions.Add(new Auction(_auctions.Count, Offices[officeNumber].Employees[executorNumber], sellerId,
            startPrice, buyerId));
    }

    private void PutExecutorUpForAuction(int playerId, int officeNumber, int executorNumber,
        int startPrice)
        => ProposeExecutor(playerId, officeNumber, executorNumber, startPrice, -1);

    private void ProposeOpportunity(int sellerId, int opportunityNumber, int startPrice, int buyerId)
    {
        var seller = FindPlayerById(sellerId);
        if (seller.IsOut || opportunityNumber < 0 || opportunityNumber >= _opportunities.Length
            || !seller.Opportunities.Contains(opportunityNumber))
        {
            throw new PmSimException("It is impossible to put the opportunity up for auction.");
        }

        _auctions.Add(
            new Auction(_auctions.Count, _opportunities[opportunityNumber], sellerId, startPrice, buyerId));
    }

    private void PutOpportunityUpForAuction(int playerId, int opportunityNumber, int startPrice)
        => ProposeOpportunity(playerId, opportunityNumber, startPrice, -1);

    /// <summary>
    /// The universal method for private and personal auctions.
    /// If the auction is private, then the seller does not participate, the bid can only increase.
    /// Otherwise, the seller can participate and raise the bid, the buyer can lower it.
    /// </summary>
    internal void ParticipateInAuction(int buyerId, int auctionNumber, int offerSum)
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
            throw new PmSimException("It is impossible to participate in the auction.");
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
    /// Returns a list of private auctions, personal offers for this player and offers made by this player.
    /// </summary>
    internal Auction[] RequestIncomingOffers(int playerId)
    {
        if (FindPlayerById(playerId).IsOut || _stage != GameStages.Diplomacy)
        {
            throw new PmSimException("It is impossible to request incoming offers.");
        }

        return _auctions.Where(x => x.IsPublic || x.SellerId == playerId || x.LastBuyerId == playerId).ToArray();
    }

    internal void MakeDecisionOnIncident(int playerId, int donation)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || player.Money < donation)
        {
            throw new PmSimException("It is impossible to make decision on the incident.");
        }

        CurrentIncident.DonationsSum += donation;
        player.Money -= donation;
    }

    private void SkipMove(int playerId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut)
        {
            throw new PmSimException("It is impossible to skip move.");
        }

        player.ActionsNumber = 0;
    }

    private void GiveUp(int playerId)
    {
        var player = FindPlayerById(playerId);
        player.IsOut = true;
    }

    internal static Project GetProjectRandomly()
        => (Project) GameConstants.Projects[(new Random()).Next(GameConstants.Projects.Length)].Clone();

    private void AssignToTask(int playerId, int officeNumber, int executorNumber, EmployeeTask task)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeNumber < 0 || officeNumber >= Offices.Length
            || Offices[officeNumber].OwnerId != playerId || executorNumber < 0
            || executorNumber >= Offices[officeNumber].Employees.Count)
        {
            throw new PmSimException("It is impossible to assign to work.");
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
            throw new PmSimException("Failed to initialize game objects.");
        }
    }

    private void CreateActors(int playersQuantity, int botsQuantity)
    {
        if (playersQuantity < 0)
        {
            throw new PmSimException("Incorrect number of players.");
        }

        if ((playersQuantity + botsQuantity < 2) ||
            (playersQuantity + botsQuantity > GameConstants.MaxActorsNumber))
        {
            throw new PmSimException("Incorrect number of bots or players.");
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
        ChangeStage(GameStages.Connection, _settings.ConnectionRealTime);
        while (Time > 0 && _players.Count < _playersQuantity)
        {
            await Task.Delay(1000);
            --Time;
        }

        _playersQuantity = _players.Count;
        var actors = Actors;
        foreach (var actor in actors)
        {
            foreach (var player in _players)
            {
                player.StatusChangeNotifier.AnotherPlayerStatus = PlayerLogic.GetPlayerStatus(actor);
            }
        }
    }

    private void SendPlayerStatusesToEachPlayer()
    {
        var statuses = Actors.Select(PlayerLogic.GetPlayerStatus).ToList();
        foreach (var player in _players)
        {
            player.StatusChangeNotifier.Players = statuses;
        }
    }

    private void ChangeStage(GameStages stage, int time)
    {
        _stage = stage;
        Time = time;
        foreach (var player in _players)
        {
            player.StatusChangeNotifier.ChangeCurrentStageAsync(stage, time);
        }
    }

    private async Task ProcessGameStageAsync(GameStages stage, int time)
    {
        _playersCompleted = 0;
        ChangeStage(stage, time);
        while (Time > 0 && _playersCompleted < _playersQuantity)
        {
            await Task.Delay(1000);
            --Time;
        }
    }

    private async Task ProcessChoosingBackgroundAsync()
        => await ProcessGameStageAsync(GameStages.ChoosingBackground, _settings.ChoosingBackgroundRealTime);

    private async Task ProcessManagementAsync()
        => await ProcessGameStageAsync(GameStages.Management, _settings.SprintRealTime);

    private async Task ProcessDiplomacyAsync()
    {
        _playersCompleted = 0;
        ChangeStage(GameStages.Diplomacy, _settings.DiplomacyRealTime);
        while (Time > 0 && _playersCompleted < _playersQuantity)
        {
            foreach (var bot in _bots)
            {
                bot.MakeDiplomaticAction();
            }

            await Task.Delay(1000);
            --Time;
        }
    }

    private void SummingUpExpenses()
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

    private void SummingUpAuctions()
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
                    throw new PmSimException("Invalid auction lot!");
            }
        }
    }

    private void SummingUpEmployeesTasks()
    {
        foreach (var office in Offices)
        {
            if (office.OwnerId == -1)
            {
                continue;
            }

            foreach (var employee in office.Employees)
            {
                EmployeeLogic.Work(employee);
            }
        }
    }

    private void ProcessSummingUpAsync()
    {
        _stage = GameStages.SummingUpResults;

        SummingUpExpenses();
        SummingUpAuctions();
        SummingUpEmployeesTasks();

        foreach (var player in Actors)
        {
            PlayerLogic.SummingUp(player);
        }
    }

    private async Task ProcessIncidentAsync()
    {
        CurrentIncident = _incidents[_random.Next(_incidents.Length)];
        await ProcessGameStageAsync(GameStages.IncidentDiscussion, _settings.IncidentRealTime);
        foreach (var bot in _bots)
        {
            bot.MakeIncidentDecision();
        }

        _stage = GameStages.IncidentResolution;
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

    private void MakeBotSprintMoves()
    {
        foreach (var bot in _bots)
        {
            if (bot.ActionsNumber > 0)
            {
                bot.MakeSprintActions();
            }
        }
    }

    private void ApproveWinner(Func<Player, bool> condition)
    {
        Winner = _players.FirstOrDefault(condition);
        if (Winner != null)
        {
            return;
        }

        Winner = _bots.FirstOrDefault(condition);
        if (Winner == null)
        {
            throw new PmSimException("There is no winner.");
        }
    }

    private void ApproveWinner()
    {
        switch (_settings.Mode)
        {
            case GameModes.Survival:
                ApproveWinner((player => !player.IsOut));
                break;
            case GameModes.TimerAndMoney:
                var max = Math.Max(_players.Max(player => player.Money),
                    _bots.Max(bot => bot.Money));
                ApproveWinner((player => player.Money == max));
                break;
            case GameModes.TimerAndProjects:
                var projects = Math.Max(_players.Max(player => player.CompletedProjects),
                    _bots.Max(bot => bot.CompletedProjects));
                ApproveWinner((player => player.CompletedProjects == projects));
                break;
            default:
                throw new PmSimException("Game mode is out of range.");
        }
    }

    private async Task ProcessAsync()
    {
        await ProcessConnectionAsync();
        SendPlayerStatusesToEachPlayer();
        await ProcessChoosingBackgroundAsync();
        while (_stage != GameStages.IsOver)
        {
            foreach (var actor in Actors)
            {
                actor.ActionsNumber = _settings.SprintActionsNumbers;
                actor.Projects.Add(GetProjectRandomly());
            }

            await ProcessManagementAsync();
            MakeBotSprintMoves();
            await ProcessDiplomacyAsync();
            ProcessSummingUpAsync();
            await ProcessIncidentAsync();
        }

        ApproveWinner();
    }

    private Interview FindInterviewById(int playerId)
    {
        var interview = _interviews.FirstOrDefault(x => x.PlayerId == playerId);
        if (interview == null)
        {
            throw new PmSimException("There is no such player.");
        }

        return interview;
    }

    private Player FindPlayerById(int playerId)
    {
        var player = Actors.FirstOrDefault(x => x.Id == playerId);
        if (player == null)
        {
            throw new PmSimException("There is no such player.");
        }

        return player;
    }

    private static IGameMap FindMap(int number)
        => number switch
        {
            _ => new Map0()
        };

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