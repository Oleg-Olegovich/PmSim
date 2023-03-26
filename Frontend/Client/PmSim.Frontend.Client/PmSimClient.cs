using PmSim.Shared.Contracts.Account;
using PmSim.Shared.Contracts.Credentials;
using PmSim.Shared.Contracts.Enums;
using PmSim.Frontend.Client.Dto;

namespace PmSim.Frontend.Client;

public class PmSimClient
{
    //private readonly IGatewayClient _gateway;
    private readonly ClientOptions _clientOptions;
    
    /// <summary>
    /// For single mode.
    /// </summary>
    public PmSimClient(string playerName)
    {
        _clientOptions = new ClientOptions();
        //PlayerName = playerName;
        //_gateway = new Backend.Gateway.SingleModeApi.GatewayClient();
    }

    /// <summary>
    /// For multiplayer mode.
    /// </summary>
    private PmSimClient()
    {
        _clientOptions = ConfigurationManager.ClientOptions;
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

    public static async Task<bool> ReserveLogin(string login)
    {
        // Проверяет, не занят ли логин. Если нет - возвращает true и резервирует, чтобы никто другой не занял.
        return true;
    }
    
    public static async Task<string> SendCodeToEmailAsync(string email)
    {
        var code = new Random().Next(100000, 999999);
        // Отправляет Email с кодом. Возращает код, который был отправлен на почту.
        return code.ToString();
    }

    public static async Task<PmSimClient> SignUpAsync(User user)
    {
        var request = new CreateAccountRequest();
        //var response = await _gateway.CreateAccountAsync(request);
        return new PmSimClient();
    }

    public static async Task<PmSimClient> SignInAsync(string login, string password)
    {
        var request = new AuthorizationRequest();
        //var response = await _gateway.GetAccountAsync(request);
        return new PmSimClient();
    }

    public async Task CreateNewGameAsync(GameSettings gameSettings)
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

    public async Task ConnectToGame(int gameId)
    {
        
    }

    public async Task SetBackgroundAsync()
    {
        
    }

    public async Task CancelOfficeLeaseAsync()
    {
    }

    public async Task DismissAllEmployeesAsync()
    {
    }

    public async Task ConductInterviewAsync()
    {
    }

    public async Task ProcessInterviewAsync()
    {
    }

    public async Task HireTechSupportOfficerAsync()
    {
    }

    public async Task DismissTechSupportOfficerAsync()
    {
    }

    public async Task UseOpportunityAsync()
    {
    }

    public async Task AssignToWorkAsync()
    {
    }

    public async Task AssignToInventProjectAsync()
    {
    }

    public async Task AssignToMakeBackupAsync()
    {
    }

    public async Task CancelTaskAsync()
    {
    }

    public async Task PutProjectUpForAuctionAsync()
    {
    }

    public async Task ProposeProjectAsync()
    {
    }

    public async Task PutExecutorUpForAuctionAsync()
    {
    }

    public async Task ProposeExecutorAsync()
    {
    }

    public async Task PutOpportunityUpForAuctionAsync()
    {
    }

    public async Task ProposeOpportunityAsync()
    {
    }

    public async Task SendMessageAsync()
    {
    }

    public async Task SendMessageToEveryoneAsync()
    {
    }

    public async Task ParticipateInAuctionAsync()
    {
    }

    public async Task SetIncidentActionAsync()
    {
    }

    public async Task SkipMove()
    {
    }

    public async Task ExitGame()
    {
    }

    public async Task RentOfficeAsync(int officeNumber)
    {
    }

    public Game[] GetActiveGames()
    {
        var games = new Game[]
        {
            new Game(0, "Тест", "Survival", "The simple village", 0, 10),
            new Game(1, "Test", "TimerAndMoney", "The simple village", 0, 5)
        };
        return games;
    }

    public string[] GetModes()
        => Enum.GetNames(typeof(GameModes));

    public string[] GetMaps()
    {
        return new string[] { "The simple village" };
    }
}