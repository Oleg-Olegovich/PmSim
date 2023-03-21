using PmSim.Backend.Gateway.Contracts.Account;
using PmSim.Backend.Gateway.Contracts.Actions;
using PmSim.Backend.Gateway.Contracts.Credentials;
using PmSim.Backend.Gateway.Contracts.Enums;
using PmSim.Backend.Gateway.Contracts.Game;
using PmSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

namespace PmSim.Frontend.Client.Interfaces;

public interface IPmSimClient
{
    string PlayerName { get; set; }

    GameStages GameStage { get; }

    int GameStageLabel { get; }

    int Time { get; }

    PlayerModel PlayerActor { get; }

    ActorModel[] Actors { get; }

    int Offices { get; }

    int Employees { get; }

    Task<IsOkResponse> CreateAccountAsync(CreateAccountRequest model);

    Task<TokenResponse> SignInAsync(AuthorizationRequest model);

    Task<int> CreateNewGameAsync(int maxPlayersNumber, int botsNumber, int mode);

    Task<int> CreateNewGameAsync(int maxPlayersNumber, int botsNumber, int mode, SettingsModel settings);

    Task ConnectToGame(int gameId);

    Task<IsOkResponse> SetBackgroundAsync(SetBackgroundRequest request);

    Task<IsOkResponse> CancelOfficeLeaseAsync(OfficeActionRequest request);

    Task<IsOkResponse> DismissAllEmployeesAsync(OfficeActionRequest request);

    Task<EmployeeInfoResponse> ConductInterviewAsync(OfficeActionRequest request);

    Task<IsOkResponse> ProcessInterviewAsync(InterviewActionRequest request);

    Task<IsOkResponse> HireTechSupportOfficerAsync(OfficeActionRequest request);

    Task<IsOkResponse> DismissTechSupportOfficerAsync(OfficeActionRequest request);

    Task<IsOkResponse> UseOpportunityAsync(OpportunityActionRequest request);

    Task<IsOkResponse> AssignToWorkAsync(DevelopmentActionRequest request);

    Task<IsOkResponse> AssignToInventProjectAsync(ExecutorActionRequest request);

    Task<IsOkResponse> AssignToMakeBackupAsync(FeaturesActionRequest request);

    Task<IsOkResponse> CancelTaskAsync(ExecutorActionRequest request);

    Task<IsOkResponse> PutProjectUpForAuctionAsync(ProjectAuctionActionRequest request);

    Task<IsOkResponse> ProposeProjectAsync(ProposeProjectActionRequest request);

    Task<IsOkResponse> PutExecutorUpForAuctionAsync(ExecutorAuctionActionRequest request);

    Task<IsOkResponse> ProposeExecutorAsync(ProposeExecutorActionRequest request);

    Task<IsOkResponse> PutOpportunityUpForAuctionAsync(OpportunityAuctionActionRequest request);

    Task<IsOkResponse> ProposeOpportunityAsync(ProposeOpportunityActionRequest request);

    Task<IsOkResponse> SendMessageAsync(SendMessageActionRequest request);

    Task<IsOkResponse> SendMessageToEveryoneAsync(SendMessageToEveryoneActionRequest request);

    Task<IsOkResponse> ParticipateInAuctionAsync(AuctionActionRequest request);

    Task<IsOkResponse> SetIncidentActionAsync(IncidentActionRequest request);

    Task<IsOkResponse> SkipMove(ActionRequest request);

    Task<IsOkResponse> ExitGame(ActionRequest request);

    Task UpdateStatusAsync();

    OfficeModel GetOffice(int number);

    bool IsOfficeMine(int number);

    Task<bool> RentOfficeAsync(int officeNumber);

    ActorModel GetPlayer(int id);
}