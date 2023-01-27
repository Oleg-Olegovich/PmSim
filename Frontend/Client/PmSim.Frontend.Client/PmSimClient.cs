using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PmSim.Backend.GameEngine.Enums;
using PmSim.Backend.Gateway.Api;
using PmSim.Backend.Gateway.Contracts.Account;
using PmSim.Backend.Gateway.Contracts.Actions;
using PmSim.Backend.Gateway.Contracts.Credentials;
using PmSim.Backend.Gateway.Contracts.Game;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;
using PmSim.Backend.Gateway.Contracts.Game.Status;
using PmSim.Frontend.Client.Interfaces;
using PmSim.Frontend.Domain.Interfaces;

namespace PmSim.Frontend.Domain
{
    public class PmSimClient : IPmSimClient
    {
        private readonly IGatewayClient _gateway;
        private int _gameId, _playerId = 0;
        private string _playerToken = "";
        private GameStatusModel _gameStatus;

        public string PlayerName { get; set; }

        public GameStages GameStage => _gameStatus.Stage;

        public int GameStageLabel => (int)_gameStatus.Stage;

        public int Time => _gameStatus.Time < 0 ? 0 : _gameStatus.Time;

        public PlayerModel PlayerActor => _gameStatus.Player;

        public ActorModel[] Actors => _gameStatus.Actors;

        public int Offices => _gameStatus.Offices.Count(x => x.OwnerId == _playerId);

        public int Employees
            => _gameStatus.Offices.Where(office => office.OwnerId == _playerId).Sum(office => office.Capacity);

        /// <summary>
        /// For single mode.
        /// </summary>
        public PmSimClient()
        {
            _gateway = new Backend.Gateway.SingleModeApi.GatewayClient();
        }

        /// <summary>
        /// For multiplayer mode.
        /// </summary>
        public PmSimClient(IOptions<ClientOptions> options)
        {
            var gatewayClientOptions = new OptionsWrapper<GatewayClientOptions>(
                new GatewayClientOptions
                {
                    BaseUri = options.Value.BaseUri
                });
            _gateway = new Backend.Gateway.MultiplayerModeApi.GatewayClient(gatewayClientOptions);
        }

        public async Task<IsOkResponse> CreateAccountAsync(CreateAccountRequest request)
        {
            return await _gateway.CreateAccountAsync(request);
        }

        public async Task<TokenResponse> SignInAsync(AuthorizationRequest request)
        {
            return await _gateway.GetAccountAsync(request);
        }

        public async Task<int> CreateNewGameAsync(int maxPlayersNumber, int botsNumber, int mode)
        {
            var request = new CreateGameRequest()
            {
                Founder = PlayerName,
                MaxPlayersNumber = maxPlayersNumber,
                BotsNumber = botsNumber,
                Mode = mode,
                IsDefaultSettings = true
            };
            var gameModel = await _gateway.CreateNewGameAsync(request);
            _gameId = gameModel.Id;
            return _gameId;
        }

        public async Task<int> CreateNewGameAsync(int maxPlayersNumber, int botsNumber, int mode,
            SettingsModel settings)
        {
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
        }

        public async Task ConnectToGame(int gameId)
        {
            var request = new ActionRequest()
            {
                GameId = _gameId,
                PlayerId = _playerId,
                PlayerToken = _playerToken
            };
            var response = await _gateway.ConnectToGame(request);
            if (!response.IsOk)
            {
                throw new Exception("Unsuccessful connection.");
            }
        }

        public async Task<IsOkResponse> SetBackgroundAsync(SetBackgroundRequest request)
        {
            return await _gateway.SetBackgroundAsync(request);
        }

        public async Task<IsOkResponse> CancelOfficeLeaseAsync(OfficeActionRequest request)
        {
            return await _gateway.CancelOfficeLeaseAsync(request);
        }

        public async Task<IsOkResponse> DismissAllEmployeesAsync(OfficeActionRequest request)
        {
            return await _gateway.DismissAllEmployeesAsync(request);
        }

        public async Task<EmployeeInfoResponse> ConductInterviewAsync(OfficeActionRequest request)
        {
            return await _gateway.ConductInterviewAsync(request);
        }

        public async Task<IsOkResponse> ProcessInterviewAsync(InterviewActionRequest request)
        {
            return await _gateway.ProcessInterviewAsync(request);
        }

        public async Task<IsOkResponse> HireTechSupportOfficerAsync(OfficeActionRequest request)
        {
            return await _gateway.HireTechSupportOfficerAsync(request);
        }

        public async Task<IsOkResponse> DismissTechSupportOfficerAsync(OfficeActionRequest request)
        {
            return await _gateway.DismissTechSupportOfficerAsync(request);
        }

        public async Task<IsOkResponse> UseOpportunityAsync(OpportunityActionRequest request)
        {
            return await _gateway.UseOpportunityAsync(request);
        }

        public async Task<IsOkResponse> AssignToWorkAsync(DevelopmentActionRequest request)
        {
            return await _gateway.AssignToWorkAsync(request);
        }

        public async Task<IsOkResponse> AssignToInventProjectAsync(ExecutorActionRequest request)
        {
            return await _gateway.AssignToInventProjectAsync(request);
        }

        public async Task<IsOkResponse> AssignToMakeBackupAsync(FeaturesActionRequest request)
        {
            return await _gateway.AssignToMakeBackupAsync(request);
        }

        public async Task<IsOkResponse> CancelTaskAsync(ExecutorActionRequest request)
        {
            return await _gateway.CancelTaskAsync(request);
        }

        public async Task<IsOkResponse> PutProjectUpForAuctionAsync(ProjectAuctionActionRequest request)
        {
            return await _gateway.PutProjectUpForAuctionAsync(request);
        }

        public async Task<IsOkResponse> ProposeProjectAsync(ProposeProjectActionRequest request)
        {
            return await _gateway.ProposeProjectAsync(request);
        }

        public async Task<IsOkResponse> PutExecutorUpForAuctionAsync(ExecutorAuctionActionRequest request)
        {
            return await _gateway.PutExecutorUpForAuctionAsync(request);
        }

        public async Task<IsOkResponse> ProposeExecutorAsync(ProposeExecutorActionRequest request)
        {
            return await _gateway.ProposeExecutorAsync(request);
        }

        public async Task<IsOkResponse> PutOpportunityUpForAuctionAsync(OpportunityAuctionActionRequest request)
        {
            return await _gateway.PutOpportunityUpForAuctionAsync(request);
        }

        public async Task<IsOkResponse> ProposeOpportunityAsync(ProposeOpportunityActionRequest request)
        {
            return await _gateway.ProposeOpportunityAsync(request);
        }

        public async Task<IsOkResponse> SendMessageAsync(SendMessageActionRequest request)
        {
            return await _gateway.SendMessageAsync(request);
        }

        public async Task<IsOkResponse> SendMessageToEveryoneAsync(SendMessageToEveryoneActionRequest request)
        {
            return await _gateway.SendMessageToEveryoneAsync(request);
        }

        public async Task<IsOkResponse> ParticipateInAuctionAsync(AuctionActionRequest request)
        {
            return await _gateway.ParticipateInAuctionAsync(request);
        }

        public async Task<IsOkResponse> SetIncidentActionAsync(IncidentActionRequest request)
        {
            return await _gateway.SetIncidentActionAsync(request);
        }

        public async Task<IsOkResponse> SkipMove(ActionRequest request)
        {
            return await _gateway.SkipMove(request);
        }

        public async Task<IsOkResponse> ExitGame(ActionRequest request)
        {
            return await _gateway.ExitGame(request);
        }

        public async Task UpdateStatusAsync()
        {
            var request = new ActionRequest()
            {
                GameId = _gameId,
                PlayerToken = _playerToken
            };
            _gameStatus = await _gateway.GetGameStatusAsync(request);
        }

        public OfficeModel GetOffice(int number)
        {
            if (number < 0 || number >= _gameStatus.Offices.Length)
            {
                throw new ArgumentException("Invalid office number.");
            }

            return _gameStatus.Offices[number];
        }

        public bool IsOfficeMine(int number) => GetOffice(number).OwnerId == _playerId;

        public async Task<bool> RentOfficeAsync(int officeNumber)
        {
            return (await _gateway.RentOfficeAsync(new OfficeActionRequest()
            {
                OfficeNumber = officeNumber
            })).IsOk;
        }

        public ActorModel GetPlayer(int id)
        {
            if (id < 0 || id >= _gameStatus.Offices.Length)
            {
                throw new ArgumentException("Invalid player id.");
            }

            return _gameStatus.Actors.FirstOrDefault(x => x.Id == id);
        }
    }
}