using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Frontend.Client.Api;

public interface IPmSimClient
{
    public static string[] GetModes()
        => new[]
        {
            LocalizationGameModes.ResourceManager.GetString(GameModes.Survival.ToString())!,
            LocalizationGameModes.ResourceManager.GetString(GameModes.TimerAndMoney.ToString())!,
            LocalizationGameModes.ResourceManager.GetString(GameModes.TimerAndProjects.ToString())!
        };
    
    public static string[] GetBackgrounds()
        => new[]
        {
            LocalizationProfessions.ResourceManager.GetString(Professions.Major.ToString())!,
            LocalizationProfessions.ResourceManager.GetString(Professions.Programmer.ToString())!,
            LocalizationProfessions.ResourceManager.GetString(Professions.Designer.ToString())!,
            LocalizationProfessions.ResourceManager.GetString(Professions.Musician.ToString())!,
            LocalizationProfessions.ResourceManager.GetString(Professions.Manager.ToString())!
        };

    public static string[] GetMaps()
        => new[]
        {
            LocalizationGameMaps.Map0
        };

    public void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic);

    public void SetBackground(Professions profession);

    public void CancelOfficeLease();

    public void DismissAllEmployees();

    public void ConductInterview();

    public void ProcessInterview();

    public void HireTechSupportOfficer();

    public void DismissTechSupportOfficer();

    public void UseOpportunity();

    public void AssignToWork();

    public void AssignToInventProject();

    public void AssignToMakeBackup();

    public void CancelTask();

    public void PutProjectUpForAuction();

    public void ProposeProject();

    public void PutExecutorUpForAuction();

    public void ProposeExecutor();

    public void PutOpportunityUpForAuction();

    public void ProposeOpportunity();

    public void SendMessage();

    public void SendMessageToEveryone();

    public void ParticipateInAuction();

    public void SetIncidentAction();

    public void SkipMove();

    public void ExitGame();

    public void RentOfficeAsync(int officeNumber);
}