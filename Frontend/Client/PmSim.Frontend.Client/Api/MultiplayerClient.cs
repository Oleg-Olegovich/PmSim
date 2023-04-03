using PmSim.Frontend.Client.Dto;
using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Credentials;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Interfaces;
using PmSim.Shared.Contracts.User;

namespace PmSim.Frontend.Client.Api;

public class MultiplayerClient : IPmSimClient
{
    //private readonly IGatewayClient _gateway;
    private readonly ClientOptions _clientOptions;

    private MultiplayerClient()
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

    public static MultiplayerClient SignUp(User user)
    {
        //var response = await _gateway.CreateAccountAsync(request);
        return new MultiplayerClient();
    }

    public static MultiplayerClient SignInAsync(string login, string password)
    {
        var request = new AuthorizationRequest();
        //var response = await _gateway.GetAccountAsync(request);
        return new MultiplayerClient();
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

    public void CancelOfficeLease()
    {
    }

    public void DismissAllEmployees()
    {
    }

    public void ConductInterview()
    {
    }

    public void ProcessInterview()
    {
    }

    public void HireTechSupportOfficer()
    {
    }

    public void DismissTechSupportOfficer()
    {
    }

    public void UseOpportunity()
    {
    }

    public void AssignToWork()
    {
    }

    public void AssignToInventProject()
    {
    }

    public void AssignToMakeBackup()
    {
    }

    public void CancelTask()
    {
    }

    public void PutProjectUpForAuction()
    {
    }

    public void ProposeProject()
    {
    }

    public void PutExecutorUpForAuction()
    {
    }

    public void ProposeExecutor()
    {
    }

    public void PutOpportunityUpForAuction()
    {
    }

    public void ProposeOpportunity()
    {
    }

    public void SendMessage()
    {
    }

    public void SendMessageToEveryone()
    {
    }

    public void ParticipateInAuction()
    {
    }

    public void SetIncidentAction()
    {
    }

    public void SkipMove()
    {
    }

    public void ExitGame()
    {
    }

    public void RentOfficeAsync(int officeNumber)
    {
    }

    public GameModel[] GetActiveGames()
    {
        var games = new GameModel[]
        {
            new(0, "Вася", "Тест", LocalizationGameModes.Survival, LocalizationGameMaps.Map0, 0, 10),
            new(1, "Петя", "Test", LocalizationGameModes.TimerAndMoney, LocalizationGameMaps.Map0, 0, 5)
        };
        return games;
    }
}