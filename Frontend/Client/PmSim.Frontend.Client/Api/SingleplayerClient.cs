using PmSim.Shared.Contracts.Game;

namespace PmSim.Frontend.Client.Api;

public class SingleplayerClient : IPmSimClient
{
    private readonly string _playerName;
    
    public SingleplayerClient(string playerName)
    {
        _playerName = playerName;
    }

    public async Task CreateNewGameAsync(GameOptions gameOptions)
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

    public async Task SkipMoveAsync()
    {
    }

    public async Task ExitGameAsync()
    {
    }

    public async Task RentOfficeAsync(int officeNumber)
    {
    }
}