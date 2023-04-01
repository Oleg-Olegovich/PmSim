using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using PmSim.Shared.Contracts.Interfaces;
using PmSim.Shared.GameEngine;

namespace PmSim.Frontend.Client.Api;

public class SingleplayerClient : IPmSimClient, IStatusChangeNotifier
{
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

        _gameScreenLogic.GameStage = stageName;
        _timer = Task.Run(ProcessTimerAsync);
    }

    public void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic)
    {
        _gameScreenLogic = gameScreenLogic;
        _game = new Game(_playerName, 0, gameOptions);
        _game.Connect(0, _playerName, this);
    }

    public void SetBackground(Professions profession) 
        => _game?.SetBackground(0, profession);

    public void CancelOfficeLease()
    {
    }

    public void DismissAllEmployees()
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

    public void UseOpportunity()
    {
    }

    public void AssignToWork()
    {
    }

    public void AssignToInventProject()
    {
    }

    public void AssignToMakeBackup()
    {
    }

    public void CancelTask()
    {
    }

    public void PutProjectUpForAuction()
    {
    }

    public void ProposeProject()
    {
    }

    public void PutExecutorUpForAuction()
    {
    }

    public void ProposeExecutor()
    {
    }

    public void PutOpportunityUpForAuction()
    {
    }

    public void ProposeOpportunity()
    {
    }

    public void SendMessage()
    {
    }

    public void SendMessageToEveryone()
    {
    }

    public void ParticipateInAuction()
    {
    }

    public void SetIncidentAction()
    {
    }

    public void SkipMove()
    {
    }

    public void ExitGame()
    {
    }

    public void RentOfficeAsync(int officeNumber)
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