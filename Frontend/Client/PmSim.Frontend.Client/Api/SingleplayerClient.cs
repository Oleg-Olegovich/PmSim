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

    public int CurrentProjectsNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }
            
            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.CurrentProjectsNumber = value;
            }, DispatcherPriority.Background);
        }
    }
    
    public int IdeasNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }
            
            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.IdeasNumber = value;
            }, DispatcherPriority.Background);
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

            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.CurrentProjectsNumber = value;
                var status = _gameScreenLogic.Players.FirstOrDefault(player => player.Id == _playerId);
                if (status != null)
                {
                    status.CompletedProjects = value;
                }
            }, DispatcherPriority.Background);
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

            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.FailedProjectsNumber = value;
            }, DispatcherPriority.Background);
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

            Dispatcher.UIThread.Post(() =>
            {
                _gameScreenLogic.EmployeesNumber = value;
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
    
    public SingleplayerClient(string playerName) 
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
        
    }

    public void Remove(EmployeeStatus employee)
    {
        
    }

    public void Add(Project project)
    {
        
    }

    public void AddOpportunity(int number)
    {
        
    }

    public void RemoveOpportunity(int number)
    {
        
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
        => _game?.CancelOfficeLease(_playerId, officeId);
    
    public void SkipMove() => _game?.SkipMove(_playerId);

    public void GiveUp() => _game?.GiveUp(_playerId);

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