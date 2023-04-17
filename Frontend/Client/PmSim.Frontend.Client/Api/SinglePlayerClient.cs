using Avalonia.Threading;
using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;
using PmSim.Shared.Contracts.Interfaces;
using PmSim.Shared.GameEngine;

namespace PmSim.Frontend.Client.Api;

public class SinglePlayerClient : IPmSimClient, IStatusChangeNotifier
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
            
            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.ActionsNumber = value;
            }, DispatcherPriority.Background);
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

            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.Money = value;
                var status = _gameScreenLogic.Players.FirstOrDefault(player => player.Id == _playerId);
                if (status != null)
                {
                    status.Money = value;
                }
            }, DispatcherPriority.Background);
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
            
            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.OfficesNumber = value;
            }, DispatcherPriority.Background);
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

            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.TechSupportOfficersNumber = value;
            }, DispatcherPriority.Background);
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

            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.MaxEmployeesNumber = value;
            }, DispatcherPriority.Background);
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

    public List<PlayerStatus> Players
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            Dispatcher.UIThread.Post(() =>
            {
                foreach (var status in value)
                {
                    _gameScreenLogic.Players.Add(status);
                }
            }, DispatcherPriority.Background);
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
            
            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.ProcessLosing();
            }, DispatcherPriority.Background);
        }
    }
    
    public SinglePlayerClient(string playerName) 
        => _playerName = playerName;
    
    public void SetAnotherStatus(PlayerStatus player)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() =>
        {
            for (var i = 0; i < _gameScreenLogic.Players.Count; ++i)
            {
                if (_gameScreenLogic.Players[i].Id != player.Id)
                {
                    continue;
                }

                _gameScreenLogic.Players[i] = player;
                return;
            }
        }, DispatcherPriority.Background);
    }

    public void RemovePlayer(int id)
    {
        if (_gameScreenLogic is null || id < 0 || id >= _gameScreenLogic.Players.Count)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.Players[id].IsOut = true;
        }, DispatcherPriority.Background);
    }

    public void Add(EmployeeStatus employee)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.Add(employee);
        }, DispatcherPriority.Background);
    }

    public void Remove(EmployeeStatus employee)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.Remove(employee);
        }, DispatcherPriority.Background);
    }

    public void Add(Project project)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }

        project.Name = LocalizationProjectNames.ResourceManager
            .GetString(project.DescriptionNumber.ToString());    
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.Add(project);
        }, DispatcherPriority.Background);
    }
    
    public void StartProject(int id)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.StartProject(id);
        }, DispatcherPriority.Background);
    }

    public void CompleteProject(int id)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.CompleteProject(id);
        }, DispatcherPriority.Background);
    }

    public void FailProject(int id)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.FailProject(id);
        }, DispatcherPriority.Background);
    }

    public void UpdateProjectProgress(int id, ProgressPoints points)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.UpdateProjectProgress(id, points);
        }, DispatcherPriority.Background);
    }

    public void AddOpportunity(int number)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }

        var opportunity = new OpportunityModel
        {
            Number = number,
            Name = LocalizationOpportunityNames
                .ResourceManager.GetString(number.ToString()),
            Description = LocalizationOpportunityDescriptions
                .ResourceManager.GetString(number.ToString()),
            Effect = LocalizationOpportunityEffects
                .ResourceManager.GetString(number.ToString())
        };
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.AddOpportunity(opportunity);
        }, DispatcherPriority.Background);
    }

    public void RemoveOpportunity(int number)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }
            
        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.RemoveOpportunity(number);
        }, DispatcherPriority.Background);
    }

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

        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.GameStage = stage;
            _gameScreenLogic.GameStageName = stageName;
            _time = time;
            _timer = Task.Run(ProcessTimerAsync);
        }, DispatcherPriority.Background);
    }

    public void ChangeOfficeState(int officeId, OfficeStates officeState)
    {
        if (_gameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() =>
        {
            _gameScreenLogic.SetOfficeState(officeId, officeState);
        }, DispatcherPriority.Background);
    }

    public void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic)
    {
        _gameScreenLogic = gameScreenLogic;
        _game = new Game(_playerId, _playerName, gameOptions);
        _game.Connect(_playerId, _playerName, this);
    }

    public void SetBackground(Professions profession) 
        => _game?.SetBackground(_playerId, profession);

    public void DismissEmployees(int[] employeesIds) 
        => _game?.DismissEmployees(_playerId, employeesIds);

    public EmployeeStatus? ConductInterview() 
        => _game?.ConductInterview(_playerId);

    public bool ProcessInterview(int salary) 
        => _game is not null && _game.ProcessInterview(_playerId, salary);

    public void HireTechSupportOfficer() 
        => _game?.HireTechSupportOfficer(_playerId);

    public void DismissTechSupportOfficer() 
        => _game?.DismissTechSupportOfficer(_playerId);

    public void UseOpportunity(int opportunityNumber, int targetPlayer) 
        => _game?.UseOpportunity(_playerId, opportunityNumber, targetPlayer);

    public void AssignToDevelop(int employeeId, int projectNumber, Professions profession) 
        => _game?.AssignToDevelop(_playerId, employeeId, projectNumber, (int)profession);

    public void AssignToInventProject(int employeeId) 
        => _game?.AssignToInventProject(_playerId, employeeId);

    public void AssignToMakeBackup(int employeeId, int projectId) 
        => _game?.AssignToMakeBackup(_playerId, employeeId, projectId);

    public void CancelTask(int employeeId)
        => _game?.CancelTask(_playerId, employeeId);

    public void PutProjectUpForAuction(int projectNumber, int startPrice) 
        => _game?.PutProjectUpForAuction(_playerId, projectNumber, startPrice);

    public void PutEmployeeUpForAuction(int employeeId, int startPrice)
        => _game?.PutEmployeeUpForAuction(_playerId, employeeId, startPrice);

    public void PutOpportunityUpForAuction(int opportunityNumber, int startPrice)
        => _game?.PutOpportunityUpForAuction(_playerId, opportunityNumber, startPrice);

    public void ParticipateInAuction(int auctionNumber, int money) 
        => _game?.ParticipateInAuction(_playerId, auctionNumber, money);

    public void MakeIncidentDecision(int donation) 
        => _game?.MakeDecisionOnIncident(_playerId, donation);

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
        => _game?.CancelOfficeLease(_playerId, officeId);
    
    public void SkipMove() => _game?.SkipMove(_playerId);

    public void GiveUp() => _game?.GiveUp(_playerId);

    public void RequestStartProject(int id)
    {
        if (_game is null)
        {
            return;
        }

        _game.StartProject(_playerId, id);
    }

    private async Task ProcessTimerAsync()
    {
        for (; _time > -1; --_time)
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.Time = $"{_time / 60:D2}:{_time % 60:D2}";
            }, DispatcherPriority.Background);
            await Task.Delay(1000);
        }
    }
}