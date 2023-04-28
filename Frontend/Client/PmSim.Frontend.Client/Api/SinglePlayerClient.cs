using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Interfaces;
using PmSim.Shared.GameEngine;

namespace PmSim.Frontend.Client.Api;

public class SinglePlayerClient : BaseClient
{
    private Game? _game;

    public SinglePlayerClient(string playerName) 
        : base(playerName)
    {
    }

    public override void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic)
    {
        GameScreenLogic = gameScreenLogic;
        _game = new Game(PlayerId, PlayerName, gameOptions);
        _game.Connect(PlayerId, PlayerName, this);
    }

    public override void SetBackground(Professions profession)
        => _game?.SetBackground(PlayerId, profession);

    public override void DismissEmployees(int[] employeesIds)
        => _game?.DismissEmployees(PlayerId, employeesIds);

    public override EmployeeStatus? ConductInterview()
        => _game?.ConductInterview(PlayerId);

    public override bool ProcessInterview(int salary)
        => _game is not null && _game.ProcessInterview(PlayerId, salary);

    public override void HireTechSupportOfficer()
        => _game?.HireTechSupportOfficer(PlayerId);

    public override void DismissTechSupportOfficer()
        => _game?.DismissTechSupportOfficer(PlayerId);

    public override void UseOpportunity(int opportunityNumber, int targetPlayer)
        => _game?.UseOpportunity(PlayerId, opportunityNumber, targetPlayer);

    public override void AssignToDevelop(int employeeId, int projectNumber, Professions profession)
        => _game?.AssignToDevelop(PlayerId, employeeId, projectNumber, (int) profession);

    public override void AssignToInventProject(int employeeId)
        => _game?.AssignToInventProject(PlayerId, employeeId);

    public override void AssignToMakeBackup(int employeeId, int projectId)
        => _game?.AssignToMakeBackup(PlayerId, employeeId, projectId);

    public override void CancelTask(int employeeId)
        => _game?.CancelTask(PlayerId, employeeId);

    public override void PutProjectUpForAuction(int projectNumber, int startPrice)
        => _game?.PutProjectUpForAuction(PlayerId, projectNumber, startPrice);

    public override void PutEmployeeUpForAuction(int employeeId, int startPrice)
        => _game?.PutEmployeeUpForAuction(PlayerId, employeeId, startPrice);

    public override void PutOpportunityUpForAuction(int opportunityNumber, int startPrice)
        => _game?.PutOpportunityUpForAuction(PlayerId, opportunityNumber, startPrice);

    public override void ParticipateInAuction(int auctionNumber, int money)
        => _game?.ParticipateInAuction(PlayerId, auctionNumber, money);

    public override void MakeIncidentDecision(int donation)
        => _game?.MakeDecisionOnIncident(PlayerId, donation);

    public override Office? GetOffice(int officeId)
    {
        if (_game is null || officeId < -1 || officeId >= _game.Offices.Length)
        {
            return null;
        }

        return _game.Offices[officeId];
    }

    public override OfficeStates GetOfficeState(int officeId)
    {
        var office = GetOffice(officeId);
        if (office is null || office.OwnerId != -1 && office.OwnerId != PlayerId)
        {
            return OfficeStates.NotMine;
        }

        return office.OwnerId == PlayerId ? OfficeStates.Mine : OfficeStates.Unoccupied;
    }

    public override void RentOffice(int officeId)
        => _game?.RentOffice(PlayerId, officeId);

    public override void CancelOfficeLease(int officeId)
        => _game?.CancelOfficeLease(PlayerId, officeId);

    public override void SkipMove() => _game?.SkipMove(PlayerId);

    public override void GiveUp() => _game?.GiveUp(PlayerId);

    public override void RequestStartProject(int id) 
        => _game?.StartProject(PlayerId, id);
}