using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
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
            var status = _gameScreenLogic.Players.FirstOrDefault(player => player.Id == 0);
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

    public int ProjectsNumber
    {
        set
        {
            if (_gameScreenLogic is null)
            {
                return;
            }

            _gameScreenLogic.ProjectsNumber = value;
            var status = _gameScreenLogic.Players.FirstOrDefault(player => player.Id == 0);
            if (status != null)
            {
                status.CompletedProjects = value;
            }
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

    public void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic)
    {
        _gameScreenLogic = gameScreenLogic;
        _game = new Game(_playerName, 0, gameOptions);
        _game.Connect(0, _playerName, this);
    }

    public void SetBackground(Professions profession) 
        => _game?.SetBackground(0, profession);

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

    public void UseOpportunity(int opportunityNumber)
    {
    }

    public void AssignToWork(int employeeNumber, int projectNumber, Professions profession)
    {
    }

    public void AssignToInventProject(int employeeNumber)
    {
    }

    public void AssignToMakeBackup(int employeeNumber)
    {
    }

    public void CancelTask(int employeeNumber)
    {
    }

    public void PutProjectUpForAuction(int projectNumber)
    {
    }

    public void ProposeProject(int projectNumber)
    {
    }

    public void PutEmployeeUpForAuction(int employeeNumber)
    {
    }

    public void ProposeEmployee(int employeeNumber)
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

    public Office? GetOffice(int officeNumber)
    {
        if (_game is null || officeNumber < -1 || officeNumber >= _game.Offices.Length)
        {
            return null;
        }

        return _game.Offices[officeNumber];
    }

    public bool IsOfficeMine(int officeNumber) 
        => GetOffice(officeNumber)?.OwnerId == 0;

    public void RentOffice(int officeNumber)
    {
        var office = GetOffice(officeNumber);
        if (office is null)
        {
            throw new PmSimException("Invalid office number!");
        }
        
        office.OwnerId = 0;
        Money -= office.RentalPrice;
    }
    
    public void CancelOfficeLease(int officeNumber)
    {
    }
    
    public void SkipMove()
    {
    }

    public void ExitGame()
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