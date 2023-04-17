using PmSim.Frontend.Client.Dto;
using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Credentials;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Interfaces;
using PmSim.Shared.Contracts.User;

namespace PmSim.Frontend.Client.Api;

public class MultiPlayerClient : IPmSimClient
{
    //private readonly IGatewayClient _gateway;
    private readonly ClientOptions _clientOptions;

    public bool IsSubscriptionPaid
        => true;
    
    private MultiPlayerClient()
    {
        _clientOptions = new ClientOptions();
        /*
        PlayerName = playerName;
        var gatewayClientOptions = new OptionsWrapper<GatewayClientOptions>(
            new GatewayClientOptions
            {
                BaseUri = options.Value.BaseUri
            });
        _gateway = new Backend.Gateway.MultiplayerModeApi.GatewayClient(gatewayClientOptions);
        */
    }

    public static bool ReserveLogin(string login)
    {
        // Проверяет, не занят ли логин. Если нет - возвращает true и резервирует, чтобы никто другой не занял.
        return true;
    }
    
    public static string SendCodeToEmailAsync(string email)
    {
        var code = new Random().Next(100000, 999999);
        // Отправляет Email с кодом. Возращает код, который был отправлен на почту.
        return code.ToString();
    }

    public static MultiPlayerClient SignUp(User user)
    {
        //var response = await _gateway.CreateAccountAsync(request);
        return new MultiPlayerClient();
    }

    public static MultiPlayerClient SignInAsync(string login, string password)
    {
        var request = new AuthorizationRequest();
        //var response = await _gateway.GetAccountAsync(request);
        return new MultiPlayerClient();
    }

    public void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic)
    {
        _clientOptions.GameId = 0;
        /*
        var request = new CreateGameRequest()
        {
            Founder = PlayerName,
            MaxPlayersNumber = maxPlayersNumber,
            BotsNumber = botsNumber,
            Mode = mode,
            IsDefaultSettings = false,
            Settings = settings
        };
        var gameModel = await _gateway.CreateNewGameAsync(request);
        _gameId = gameModel.Id;
        return _gameId;
        //*/
    }

    public void ConnectToGame(int gameId)
    {
        
    }

    public void SetBackground(Professions profession)
    {
        
    }

    public void DismissEmployees(int[] employeesIds)
    {
    }

    public EmployeeStatus? ConductInterview()
    {
        return null;
    }

    public bool ProcessInterview(int salary)
    {
        return false;
    }

    public void HireTechSupportOfficer()
    {
    }

    public void DismissTechSupportOfficer()
    {
    }

    public void UseOpportunity(int opportunityNumber, int targetPlayer)
    {
    }

    public void AssignToDevelop(int employeeId, int projectNumber, Professions profession)
    {
    }

    public void AssignToInventProject(int employeeId)
    {
    }

    public void AssignToMakeBackup(int employeeId, int projectId)
    {
    }

    public void CancelTask(int employeeId)
    {
    }

    public void PutProjectUpForAuction(int projectNumber, int startPrice)
    {
    }

    public void PutEmployeeUpForAuction(int employeeId, int startPrice)
    {
    }

    public void PutOpportunityUpForAuction(int opportunityNumber, int startPrice)
    {
    }

    public void ParticipateInAuction(int auctionNumber, int money)
    {
    }

    public void MakeIncidentDecision(int donation)
    {
    }

    public Office? GetOffice(int officeId)
    {
        return null;
    }

    public OfficeStates GetOfficeState(int officeId)
        => OfficeStates.Unoccupied;

    public void RentOffice(int officeId)
    {
        
    }
    
    public void CancelOfficeLease(int officeId)
    {
    }
    
    public void SkipMove()
    {
    }

    public void GiveUp()
    {
    }
    
    public void RequestStartProject(int id)
    {
        
    }

    public GameModel[] GetActiveGames()
    {
        var games = new GameModel[]
        {
            new(0, "Вася", "Тест", GameModes.Survival, GameMaps.TheSimpleVillage, 0, 10),
            new(1, "Петя", "Test", GameModes.TimerAndMoney, GameMaps.TheSimpleVillage, 0, 5)
        };
        foreach (var game in games)
        {
            game.ModeName = LocalizationGameModes.ResourceManager.GetString(game.Mode.ToString());
            game.MapName = LocalizationGameModes.ResourceManager.GetString($"Map{(int)game.Map}");
        }
        
        return games;
    }
}