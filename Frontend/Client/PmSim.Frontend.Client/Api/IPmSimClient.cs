using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
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

    public void DismissEmployees(int[] employeesIds);

    public EmployeeStatus? ConductInterview();

    public bool ProcessInterview(int salary);

    /// <summary>
    /// The maximum number of tech support staff is the number of player offices.
    /// </summary>
    public void HireTechSupportOfficer();

    public void DismissTechSupportOfficer();

    public void AssignToDevelop(int employeeId, int projectNumber, Professions profession);

    public void AssignToInventProject(int employeeId);

    public void AssignToMakeBackup(int employeeId, int projectId);

    public void CancelTask(int employeeId);

    public void PutProjectUpForAuction(int projectNumber, int startPrice);

    public void PutEmployeeUpForAuction(int employeeId, int startPrice);

    public void UseOpportunity(int opportunityNumber, int targetPlayer);
    
    public void PutOpportunityUpForAuction(int opportunityNumber, int startPrice);

    public void ParticipateInAuction(int auctionNumber, int money);

    public void MakeIncidentDecision(int donation);

    public Office? GetOffice(int officeId);
    
    public OfficeStates GetOfficeState(int officeId);
    
    public void RentOffice(int officeId);
    
    public void CancelOfficeLease(int officeId);
    
    public void SkipMove();

    public void GiveUp();

    public void RequestStartProject(int id);
}