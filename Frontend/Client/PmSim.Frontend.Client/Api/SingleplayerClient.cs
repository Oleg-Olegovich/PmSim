﻿using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.GameObjects.Employees;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Game.GameObjects.Projects;
using PmSim.Shared.Contracts.Interfaces;
using PmSim.Shared.GameEngine;

namespace PmSim.Frontend.Client.Api;

public class SingleplayerClient : IPmSimClient, IStatusChangeNotifier
{
    private readonly int _playerId = 0;
    private readonly string _playerName;

    private Game? _game;

    private IGameScreenLogic? _gameScreenLogic;

    private int _time;

    private Task? _timer;

    public int ActionsNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.ActionsNumber = value;
        }
    }

    public int Money
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.Money = value;
            var status = _gameScreenLogic.Players.FirstOrDefault(player => player.Id == _playerId);
            if (status != null)
            {
                status.Money = value;
            }
        }
    }

    public int OfficesNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.OfficesNumber = value;
        }
    }

    public int TechSupportOfficersNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.TechSupportOfficersNumber = value;
        }
    }

    public int ProjectsNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.ProjectsNumber = value;
        }
    }
    
    public int CompletedProjectsNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.ProjectsNumber = value;
            var status = _gameScreenLogic.Players.FirstOrDefault(player => player.Id == _playerId);
            if (status != null)
            {
                status.CompletedProjects = value;
            }
        }
    }

    public int FailedProjectsNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.FailedProjectsNumber = value;
        }
    }

    public int EmployeesNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.EmployeesNumber = value;
        }
    }
    
    public int MaxEmployeesNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.MaxEmployeesNumber = value;
        }
    }

    public int Programming
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }
            
            
        }
    }

    public int Music
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }


        }
    }

    public int Design
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }


        }
    }

    public int Management
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }


        }
    }

    public int Creativity
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }


        }
    }

    public IEnumerable<PlayerStatus> Players
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            foreach (var status in value)
            {
                _gameScreenLogic.Players.Add(status);
            }
        }
    }
    
    public PlayerStatus AnotherPlayerStatus
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            for (var i = 0; i < _gameScreenLogic.Players.Count; ++i)
            {
                if (_gameScreenLogic.Players[i].Id != value.Id)
                {
                    continue;
                }

                _gameScreenLogic.Players[i] = value;
                return;
            }
        }
    }

    public int RemovePlayer
    {
        set
        {
            if (_gameScreenLogic is null || value < 0 || value >= _gameScreenLogic.Players.Count)
            {
                return;
            }

            _gameScreenLogic.Players[value].IsOut = true;
        }
    }

    public EmployeeStatus AddEmployee
    {
        set
        {
            
        }
    }

    public EmployeeStatus RemoveEmployee
    {
        set
        {
            
        }
    }

    public Project Project
    {
        set
        {
            
        }
    }

    public int AddOpportunity
    {
        set
        {
            
        }
    }

    public int RemoveOpportunity
    {
        set
        {
            
        }
    }

    public bool IsOut
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.ProcessLosing();
        }
    }

    public SingleplayerClient(string playerName) 
        => _playerName = playerName;

    public async Task ChangeCurrentStageAsync(GameStages stage, int time)
    {
        var stageName = LocalizationGameStages.ResourceManager.GetString(stage.ToString());
        if (_gameScreenLogic is null || stageName is null)
        {
            return;
        }

        if (_timer is not null)
        {
            _time = -1;
            await _timer;
        }

        _gameScreenLogic.GameStage = stage;
        _gameScreenLogic.GameStageName = stageName;
        _time = time;
        _timer = Task.Run(ProcessTimerAsync);
    }

    public void ChangeOfficeState(int officeId, OfficeStates officeState)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
        
        _gameScreenLogic.SetOfficeState(officeId, officeState);
    }

    public void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic)
    {
        _gameScreenLogic = gameScreenLogic;
        _game = new Game(_playerName, _playerId, gameOptions);
        _game.Connect(_playerId, _playerName, this);
    }

    public void SetBackground(Professions profession) 
        => _game?.SetBackground(_playerId, profession);

    public void DismissEmployees(int[] employeesIds)
    {
    }

    public void ConductInterview()
    {
    }

    public void ProcessInterview()
    {
    }

    public void HireTechSupportOfficer()
    {
    }

    public void DismissTechSupportOfficer()
    {
    }

    public void UseOpportunity(int opportunityNumber)
    {
    }

    public void AssignToWork(int employeeId, int projectNumber, Professions profession)
    {
    }

    public void AssignToInventProject(int employeeId)
    {
    }

    public void AssignToMakeBackup(int employeeId)
    {
    }

    public void CancelTask(int employeeId)
    {
    }

    public void PutProjectUpForAuction(int projectNumber)
    {
    }

    public void ProposeProject(int projectNumber)
    {
    }

    public void PutEmployeeUpForAuction(int employeeId)
    {
    }

    public void ProposeEmployee(int employeeId)
    {
    }

    public void PutOpportunityUpForAuction(int opportunityNumber)
    {
    }

    public void ProposeOpportunity(int opportunityNumber)
    {
    }
    
    public void ParticipateInAuction(int auctionNumber, int money)
    {
    }

    public void MakeIncidentDecision(int donation)
    {
    }

    public Office? GetOffice(int officeId)
    {
        if (_game is null || officeId < -1 || officeId >= _game.Offices.Length)
        {
            return null;
        }

        return _game.Offices[officeId];
    }

    public OfficeStates GetOfficeState(int officeId)
    {
        var office = GetOffice(officeId);
        if (office is null || office.OwnerId != -1 && office.OwnerId != _playerId)
        {
            return OfficeStates.NotMine;
        }

        return office.OwnerId == _playerId ? OfficeStates.Mine : OfficeStates.Unoccupied;
    }

    public void RentOffice(int officeId) 
        => _game?.RentOffice(_playerId, officeId);

    public void CancelOfficeLease(int officeId)
    {
    }
    
    public void SkipMove()
    {
    }

    public void GiveUp()
    {
    }

    private async Task ProcessTimerAsync()
    {
        for (; _time > -1; --_time)
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.Time = $"{_time / 60:D2}:{_time % 60:D2}";
            await Task.Delay(1000);
        }
    }
}