using PmSim.Frontend.Client.LanguagesManager;
using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;

namespace PmSim.Frontend.Client.Api;

public interface IPmSimClient
{
    public static string[] GetModes(Languages language)
        => new[]
        {
            LocalizationGameModes.ResourceManager.GetString(GameModes.Survival.ToString())!,
            LocalizationGameModes.ResourceManager.GetString(GameModes.TimerAndMoney.ToString())!,
            LocalizationGameModes.ResourceManager.GetString(GameModes.TimerAndProjects.ToString())!
        };

    public static string[] GetMaps(Languages language)
        => new[]
        {
            LocalizationGameMaps.Map0
        };

    public Task CreateNewGameAsync(GameOptions gameOptions);

    public Task SetBackgroundAsync();

    public Task CancelOfficeLeaseAsync();

    public Task DismissAllEmployeesAsync();

    public Task ConductInterviewAsync();

    public Task ProcessInterviewAsync();

    public Task HireTechSupportOfficerAsync();

    public Task DismissTechSupportOfficerAsync();

    public Task UseOpportunityAsync();

    public Task AssignToWorkAsync();

    public Task AssignToInventProjectAsync();

    public Task AssignToMakeBackupAsync();

    public Task CancelTaskAsync();

    public Task PutProjectUpForAuctionAsync();

    public Task ProposeProjectAsync();

    public Task PutExecutorUpForAuctionAsync();

    public Task ProposeExecutorAsync();

    public Task PutOpportunityUpForAuctionAsync();

    public Task ProposeOpportunityAsync();

    public Task SendMessageAsync();

    public Task SendMessageToEveryoneAsync();

    public Task ParticipateInAuctionAsync();

    public Task SetIncidentActionAsync();

    public Task SkipMoveAsync();

    public Task ExitGameAsync();

    public Task RentOfficeAsync(int officeNumber);
}