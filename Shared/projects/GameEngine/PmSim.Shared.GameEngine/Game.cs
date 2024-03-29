﻿using System.Reflection;
using PmSim.Shared.GameEngine.GameLogic;
using PmSim.Shared.GameEngine.GameLogic.Incidents;
using PmSim.Shared.GameEngine.GameLogic.Opportunities;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Diplomacy;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Maps;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;
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
    private readonly List<Player> _players = new(), _actors = new();
    private Bot[] _bots = Array.Empty<Bot>();
    private Incident[] _incidents = Array.Empty<Incident>();
    private Opportunity[] _opportunities = Array.Empty<Opportunity>();

    private readonly List<Interview> _interviews = new();
    private readonly IGameMap _map;
    private readonly string _founder;
    private readonly int _id;
    private readonly Random _random = new();

    public Office[] Offices => _map.Offices;

    private GameStages _stage = GameStages.NotStarted;

    /// <summary>
    /// Seconds of the current stage of the game.
    /// </summary>
    private int _time;

    private Incident? _currentIncident;

    private Player? _winner;

    public GameModel Model 
        => new(_id, _founder, _settings.GameName, _settings.Mode, (GameMaps)_settings.MapNumber, 
            _players.Count, _playersQuantity);
    
    
    public List<Auction> Auctions { get; } = new();

    public Game(int id, string founder, GameOptions settings)
    {
        _map = FindMap(settings.MapNumber);
        _id = id;
        _founder = founder;
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
        statusChangeNotifier.ChangeCurrentStageAsync(_stage, _time);
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

    public void RentOffice(int playerId, int officeId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeId < 0 || officeId >= Offices.Length
            || Offices[officeId].OwnerId != -1 || player.Money < Offices[officeId].RentalPrice)
        {
            throw new PmSimException("It is impossible to rent the office.");
        }

        Offices[officeId].OwnerId = playerId;
        player.Money -= Offices[officeId].RentalPrice;
        player.TotalRentPayment += Offices[officeId].RentalPrice;
        player.MaxEmployeesNumber += Offices[officeId].Capacity;
        ++player.OfficesNumber;
        player.IsStartupOpen = true;
        SendOfficeStateToEachPlayer(officeId);
    }

    public void CancelOfficeLease(int playerId, int officeId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || officeId < 0 || officeId >= Offices.Length
            || Offices[officeId].OwnerId != playerId
            || player.MaxEmployeesNumber - Offices[officeId].Capacity < player.Employees.Count)
        {
            throw new PmSimException("It is impossible to cancel the office lease.");
        }

        Offices[officeId].OwnerId = -1;
        player.TotalRentPayment -= Offices[officeId].RentalPrice;
        player.MaxEmployeesNumber -= Offices[officeId].Capacity;
        --player.OfficesNumber;
        SendOfficeStateToEachPlayer(officeId);
    }

    public void DismissEmployees(int playerId, int[] employeesIds)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut)
        {
            throw new PmSimException("It is impossible to dismiss all employees.");
        }

        throw new NotImplementedException();
    }

    public EmployeeStatus ConductInterview(int playerId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || player.Employees.Count < player.MaxEmployeesNumber)
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
        return EmployeeLogic.GetStatus(interview.Employee);
    }

    public bool ProcessInterview(int playerId, int proposedSalary)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || player.Money < proposedSalary || proposedSalary < 1
            || player.Employees.Count < player.MaxEmployeesNumber)
        {
            throw new PmSimException("It is impossible to process the interview.");
        }

        var interview = FindInterviewById(playerId);
        var result = interview.Process(proposedSalary);
        if (result)
        {
            player.Employees.Add(interview.Employee);
        }

        _interviews.Remove(interview);
        return result;
    }

    public void HireTechSupportOfficer(int playerId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || player.TechSupportOfficersNumber == player.OfficesNumber)
        {
            throw new PmSimException("It is impossible to hire the tech support officer.");
        }

        ++player.TechSupportOfficersNumber;
    }

    public void DismissTechSupportOfficer(int playerId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || player.TechSupportOfficersNumber == 0)
        {
            throw new PmSimException("It is impossible to dismiss the tech support officer.");
        }

        --player.TechSupportOfficersNumber;
    }

    public void UseOpportunity(int playerId, int opportunityNumber, int targetPlayer)
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

    public void AssignToDevelop(int playerId, int employeeId, int featureNumber, int progressPointNumber)
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

        AssignToTask(playerId, employeeId,
            new EmployeeDevelopmentTask(player, player.Projects[featureNumber], progressPointNumber));
    }

    public void AssignToInventProject(int playerId, int employeeId)
        => AssignToTask(playerId, employeeId, new EmployeeTask(FindPlayerById(playerId)));

    public void AssignToMakeBackup(int playerId, int employeeId, int projectId)
    {
        var player = FindPlayerById(playerId);
        if (projectId < 0 || projectId >= player.Projects.Count)
        {
            throw new PmSimException("There is no such feature.");
        }

        AssignToTask(playerId, employeeId,
            new EmployeeBackUpTask(player, player.Projects[projectId]));
    }

    public void CancelTask(int playerId, int employeeId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || employeeId < 0 || employeeId >= player.Employees.Count)
        {
            throw new PmSimException("It is impossible to cancel the task.");
        }

        player.Employees[employeeId].CurrentTask = null;
    }

    public void StartProject(int playerId, int projectId)
    {
        var player = FindPlayerById(playerId);
        if (projectId < 0 || projectId >= player.Projects.Count)
        {
            throw new PmSimException("There is no such feature.");
        }

        player.Projects[projectId].IsStart = true;
        player.StatusChangeNotifier.StartProject(projectId);
    }
    
    public void PutProjectUpForAuction(int playerId, int projectId, int startPrice)
    {
        var seller = FindPlayerById(playerId);
        if (seller.IsOut || projectId < 0 || projectId >= seller.Projects.Count
            || seller.Projects[projectId].IsStart
            || Auctions.Any(x => x.Lot == seller.Projects[projectId]))
        {
            throw new PmSimException("It is impossible to put the project up for auction.");
        }

        Auctions.Add(new Auction(Auctions.Count, seller.Projects[projectId], playerId, startPrice));
    }

    public void PutEmployeeUpForAuction(int playerId, int employeeId, int startPrice)
    {
        var seller = FindPlayerById(playerId);
        if (seller.IsOut || employeeId < 0 || employeeId >= seller.Employees.Count
                         || Auctions.Count(x => x.Lot == seller.Employees[employeeId]) > 0)
        {
            throw new PmSimException("It is impossible to put the executor up for auction.");
        }

        Auctions.Add(new Auction(Auctions.Count, seller.Employees[employeeId], playerId, startPrice));
    }

    public void PutOpportunityUpForAuction(int playerId, int opportunityNumber, int startPrice)
    {
        var seller = FindPlayerById(playerId);
        if (seller.IsOut || opportunityNumber < 0 || opportunityNumber >= _opportunities.Length
            || !seller.Opportunities.Contains(opportunityNumber))
        {
            throw new PmSimException("It is impossible to put the opportunity up for auction.");
        }

        Auctions.Add(
            new Auction(Auctions.Count, _opportunities[opportunityNumber], playerId, startPrice));
    }

    /// <summary>
    /// The universal method for private and personal auctions.
    /// If the auction is private, then the seller does not participate, the bid can only increase.
    /// Otherwise, the seller can participate and raise the bid, the buyer can lower it.
    /// </summary>
    public void ParticipateInAuction(int buyerId, int auctionId, int offerSum)
    {
        var buyer = FindPlayerById(buyerId);
        if (buyer.IsOut || auctionId < 0 || auctionId >= Auctions.Count || Auctions[auctionId].SellerId == buyerId 
            || Auctions[auctionId].LastPrice >= offerSum || buyer.Money < offerSum)
        {
            throw new PmSimException("It is impossible to participate in the auction.");
        }

        if (Auctions[auctionId].LastBuyerId != -1)
        {
            var lastBuyer = FindPlayerById(Auctions[auctionId].LastBuyerId);
            lastBuyer.Money += Auctions[auctionId].LastPrice;
        }

        Auctions[auctionId].LastBuyerId = buyerId;
        Auctions[auctionId].LastPrice = offerSum;
        buyer.Money -= offerSum;
    }

    public void MakeDecisionOnIncident(int playerId, int donation)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || player.Money < donation || _currentIncident is null)
        {
            throw new PmSimException("It is impossible to make decision on the incident.");
        }

        _currentIncident.DonationsSum += donation;
        player.Money -= donation;
    }

    public void SkipMove(int playerId)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut)
        {
            throw new PmSimException("It is impossible to skip move.");
        }

        player.ActionsNumber = 0;
    }

    public void GiveUp(int playerId) 
        => GiveUp(FindPlayerById(playerId));
    
    private void GiveUp(Player player)
    {
        _actors.Remove(player);
        for (var i = 0; i < Offices.Length; ++i)
        {
            if (Offices[i].OwnerId == player.Id)
            {
                CancelOfficeLease(player.Id, i);
            }
        }
        if (_actors.Count < 2)
        {
            _stage = GameStages.IsOver;
        }
        if (player is not Bot)
        {
            --_playersQuantity;
        }
        player.IsOut = true;
    }

    internal static Project GetProjectRandomly()
        => new(GameConstants.Projects[new Random().Next(GameConstants.Projects.Length)]);

    private void AssignToTask(int playerId, int employeeId, EmployeeTask task)
    {
        var player = FindPlayerById(playerId);
        if (player.IsOut || employeeId < 0 || employeeId >= player.Employees.Count)
        {
            throw new PmSimException("It is impossible to assign to work.");
        }

        player.Employees[employeeId].CurrentTask = task;
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
        while (_time > 0 && _players.Count < _playersQuantity)
        {
            await Task.Delay(1000);
            --_time;
        }

        _playersQuantity = _players.Count;
    }

    private void SendPlayerStatusesToEachPlayer()
    {
        _actors.AddRange(_players);
        _actors.AddRange(_bots);
        var statuses = _actors.Select(PlayerLogic.GetStatus).ToList();
        foreach (var player in _players)
        {
            player.StatusChangeNotifier.Players = statuses;
        }
    }

    private void SendOfficeStateToEachPlayer(int officeId)
    {
        foreach (var player in _players)
        {
            var officeState = Offices[officeId].OwnerId == -1
                ? OfficeStates.Unoccupied
                : player.Id == Offices[officeId].OwnerId
                    ? OfficeStates.Mine
                    : OfficeStates.NotMine;
            player.StatusChangeNotifier.ChangeOfficeState(officeId, officeState);
        }
    }

    private void ChangeStage(GameStages stage, int time)
    {
        _stage = stage;
        _time = time;
        foreach (var player in _players)
        {
            player.StatusChangeNotifier.ChangeCurrentStageAsync(stage, time);
        }
    }

    private async Task ProcessGameStageAsync(GameStages stage, int time)
    {
        _playersCompleted = 0;
        ChangeStage(stage, time);
        while (_time > 0 && _playersCompleted < _playersQuantity && _stage != GameStages.IsOver)
        {
            await Task.Delay(1000);
            --_time;
        }
    }

    private async Task ProcessChoosingBackgroundAsync() 
        => await ProcessGameStageAsync(GameStages.ChoosingBackground, _settings.ChoosingBackgroundRealTime);

    private async Task ProcessManagementAsync()
        => await ProcessGameStageAsync(GameStages.Management, _settings.SprintRealTime);

    private void SummingUpExpenses()
    {
        foreach (var actor in _actors)
        {
            actor.Money -= actor.TechSupportOfficersNumber * GameConstants.TechSupportSalary;
            if (actor.Money < 0)
            {
                GiveUp(actor);
                continue;
            }
            foreach (var employee in actor.Employees)
            {
                actor.Money -= employee.Salary;
            }
        }
    }

    private void SummingUpAuctions()
    {
        foreach (var auction in Auctions)
        {
            if (auction.LastBuyerId == -1)
            {
                continue;
            }

            var seller = FindPlayerById(auction.SellerId);
            var buyer = FindPlayerById(auction.LastBuyerId);
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
        foreach (var employee in _actors.SelectMany(actor => actor.Employees))
        {
            EmployeeLogic.Work(employee);
        }
    }

    private void ProcessSummingUpAsync()
    {
        _stage = GameStages.SummingUpResults;

        SummingUpExpenses();
        SummingUpAuctions();
        SummingUpEmployeesTasks();

        foreach (var player in _actors)
        {
            PlayerLogic.SummingUp(player);
        }
    }

    private async Task ProcessIncidentAsync()
    {
        _currentIncident = _incidents[_random.Next(_incidents.Length)];
        await ProcessGameStageAsync(GameStages.IncidentDiscussion, _settings.IncidentRealTime);
        foreach (var bot in _bots)
        {
            bot.MakeIncidentDecision();
        }

        _stage = GameStages.IncidentResolution;
        if (_currentIncident.DonationsSum < _currentIncident.PassCost)
        {
            foreach (var actor in _actors)
            {
                _currentIncident.Process(actor);
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
        _winner = _players.FirstOrDefault(condition);
        if (_winner != null)
        {
            return;
        }

        _winner = _bots.FirstOrDefault(condition);
    }

    private void ApproveWinner()
    {
        switch (_settings.Mode)
        {
            case GameModes.Survival:
                ApproveWinner(player => !player.IsOut);
                break;
            case GameModes.TimerAndMoney:
                var max = Math.Max(_players.Max(player => player.Money),
                    _bots.Max(bot => bot.Money));
                ApproveWinner(player => !player.IsOut && player.Money == max);
                break;
            case GameModes.TimerAndProjects:
            default:
                var projects = Math.Max(_players.Max(player => player.CompletedProjects),
                    _bots.Max(bot => bot.CompletedProjects));
                ApproveWinner(player => !player.IsOut && player.CompletedProjects == projects);
                break;
        }
    }

    private void StartSprint()
    {
        foreach (var actor in _actors)
        {
            actor.Money -= actor.TotalRentPayment;
            if (actor.Money < 0)
            {
                GiveUp(actor);
                continue;
            }

            actor.ActionsNumber = _settings.ManagementActionsNumbers;
            actor.Projects.Add(GetProjectRandomly());
        }
    }
    
    private async Task ProcessAsync()
    {
        await ProcessConnectionAsync();
        SendPlayerStatusesToEachPlayer();
        await ProcessChoosingBackgroundAsync();
        var sprintNumber = 0;
        while (_stage != GameStages.IsOver)
        {
            StartSprint();
            await ProcessManagementAsync();
            MakeBotSprintMoves();
            ProcessSummingUpAsync();
            await ProcessIncidentAsync();
            if (sprintNumber == _settings.MaxSprintNumber)
            {
                _stage = GameStages.IsOver;
            }
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
        var player = _actors.FirstOrDefault(x => x.Id == playerId);
        if (player is null)
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