using PmSim.Shared.Contracts.Account;
using PmSim.Shared.Contracts.Actions;
using PmSim.Shared.Contracts.Credentials;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Status;

namespace PmSim.Backend.Gateway.Api;

public interface IGatewayClient
{
    Task<IsOkResponse> CreateAccountAsync(CreateAccountRequest request);
        
    Task<TokenResponse> GetAccountAsync(AuthorizationRequest request);

    Task<GameModel> CreateNewGameAsync(CreateGameRequest request);
        
    Task<IsOkResponse> ConnectToGame(ActionRequest request);

    Task<GameModel> GetGameAsync(ActionRequest request);

    /// <summary>
    /// Finds the game by its founder's login.
    /// </summary>
    Task<GameModel> GetGameAsync(string founder);

    Task<GameStatusModel> GetGameStatusAsync(ActionRequest request);

    Task<IsOkResponse> SkipMove(ActionRequest request);
        
    Task<IsOkResponse> ExitGame(ActionRequest request);

    Task<IsOkResponse> SetBackgroundAsync(SetBackgroundRequest request);

    Task<IsOkResponse> RentOfficeAsync(OfficeActionRequest request);
        
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

    Task<string> GetNameById(int playerId);

    /*
    protected static ActorModel CreateActorModel(Player player);
    
    protected static ActorModel[] CreateActorModels(Game game);
    
    protected static ActorModel CreateOfficeModel(Office office);
    
    protected static OfficeModel[] CreateOfficeModels(Game game);
    */
}