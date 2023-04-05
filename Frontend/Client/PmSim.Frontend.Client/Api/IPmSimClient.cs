using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
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

    public void DismissAllEmployees();

    public void ConductInterview();

    public void ProcessInterview();

    /// <summary>
    /// The maximum number of tech support staff is the number of player offices.
    /// </summary>
    public void HireTechSupportOfficer();

    public void DismissTechSupportOfficer();

    public void AssignToWork(int employeeNumber, int projectNumber, Professions profession);

    public void AssignToInventProject(int employeeNumber);

    public void AssignToMakeBackup(int employeeNumber);

    public void CancelTask(int employeeNumber);

    public void PutProjectUpForAuction(int projectNumber);

    public void ProposeProject(int projectNumber);

    public void PutEmployeeUpForAuction(int employeeNumber);

    public void ProposeEmployee(int employeeNumber);

    public void UseOpportunity(int opportunityNumber);
    
    public void PutOpportunityUpForAuction(int opportunityNumber);

    public void ProposeOpportunity(int opportunityNumber);

    public void ParticipateInAuction(int auctionNumber, int money);

    public void MakeIncidentDecision(int donation);

    public Office? GetOffice(int officeId);
    
    public OfficeStates GetOfficeState(int officeId);
    
    public void RentOffice(int officeId);
    
    public void CancelOfficeLease(int officeId);
    
    public void SkipMove();

    public void ExitGame();
}