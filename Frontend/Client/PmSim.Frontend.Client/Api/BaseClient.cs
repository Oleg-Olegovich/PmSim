using Avalonia.Threading;
using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;
using PmSim.Shared.Contracts.Game.Employees;
using PmSim.Shared.Contracts.Game.Others;
using PmSim.Shared.Contracts.Game.Projects;
using PmSim.Shared.Contracts.Interfaces;

namespace PmSim.Frontend.Client.Api;

public abstract class BaseClient : IStatusChangeNotifier
{
    protected int PlayerId = 0;
    
    protected readonly string PlayerName;

    protected IGameScreenLogic? GameScreenLogic;

    private int _time;

    private Task? _timer;
    
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
    
    public int ActionsNumber
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }
            
            Dispatcher.UIThread.Post(() =>
            {
                GameScreenLogic.ActionsNumber = value;
            }, DispatcherPriority.Background);
        }
    }

    public int Money
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }

            Dispatcher.UIThread.Post(() =>
            {
                GameScreenLogic.Money = value;
                var status = GameScreenLogic.Players.FirstOrDefault(player => player.Id == PlayerId);
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
            if (GameScreenLogic is null)
            {
                return;
            }
            
            Dispatcher.UIThread.Post(() =>
            {
                GameScreenLogic.OfficesNumber = value;
            }, DispatcherPriority.Background);
        }
    }

    public int TechSupportOfficersNumber
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }

            Dispatcher.UIThread.Post(() =>
            {
                GameScreenLogic.TechSupportOfficersNumber = value;
            }, DispatcherPriority.Background);
        }
    }

    public int MaxEmployeesNumber
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }

            Dispatcher.UIThread.Post(() =>
            {
                GameScreenLogic.MaxEmployeesNumber = value;
            }, DispatcherPriority.Background);
        }
    }

    public int Programming
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }
            
            
        }
    }

    public int Music
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }


        }
    }

    public int Design
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }


        }
    }

    public int Management
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }


        }
    }

    public int Creativity
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }


        }
    }

    public List<PlayerStatus> Players
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }

            Dispatcher.UIThread.Post(() =>
            {
                foreach (var status in value)
                {
                    GameScreenLogic.Players.Add(status);
                }
            }, DispatcherPriority.Background);
        }
    }
    
    public bool IsOut
    {
        set
        {
            if (GameScreenLogic is null)
            {
                return;
            }
            
            Dispatcher.UIThread.Post(() =>
            {
                GameScreenLogic.ProcessLosing();
            }, DispatcherPriority.Background);
        }
    }

    protected BaseClient() 
        => PlayerName = "";
    
    protected BaseClient(string playerName) 
        => PlayerName = playerName;
    
    public abstract void CreateNewGame(GameOptions gameOptions, IGameScreenLogic gameScreenLogic);

    public abstract void SetBackground(Professions profession);

    public abstract void DismissEmployees(int[] employeesIds);

    public abstract EmployeeStatus? ConductInterview();

    public abstract bool ProcessInterview(int salary);

    /// <summary>
    /// The maximum number of tech support staff is the number of player offices.
    /// </summary>
    public abstract void HireTechSupportOfficer();

    public abstract void DismissTechSupportOfficer();

    public abstract void AssignToDevelop(int employeeId, int projectNumber, Professions profession);

    public abstract void AssignToInventProject(int employeeId);

    public abstract void AssignToMakeBackup(int employeeId, int projectId);

    public abstract void CancelTask(int employeeId);

    public abstract void PutProjectUpForAuction(int projectNumber, int startPrice);

    public abstract void PutEmployeeUpForAuction(int employeeId, int startPrice);

    public abstract void UseOpportunity(int opportunityNumber, int targetPlayer);
    
    public abstract void PutOpportunityUpForAuction(int opportunityNumber, int startPrice);

    public abstract void ParticipateInAuction(int auctionNumber, int money);

    public abstract void MakeIncidentDecision(int donation);

    public abstract Office? GetOffice(int officeId);
    
    public abstract OfficeStates GetOfficeState(int officeId);
    
    public abstract void RentOffice(int officeId);
    
    public abstract void CancelOfficeLease(int officeId);
    
    public abstract void SkipMove();

    public abstract void GiveUp();

    public abstract void RequestStartProject(int id);
    
    public void SetAnotherStatus(PlayerStatus player)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() =>
        {
            for (var i = 0; i < GameScreenLogic.Players.Count; ++i)
            {
                if (GameScreenLogic.Players[i].Id != player.Id)
                {
                    continue;
                }

                GameScreenLogic.Players[i] = player;
                return;
            }
        }, DispatcherPriority.Background);
    }

    public void RemovePlayer(int id)
    {
        if (GameScreenLogic is null || id < 0 || id >= GameScreenLogic.Players.Count)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.Players[id].IsOut = true; }, DispatcherPriority.Background);
    }

    public void Add(EmployeeStatus employee)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.Add(employee); }, DispatcherPriority.Background);
    }

    public void Remove(EmployeeStatus employee)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.Remove(employee); }, DispatcherPriority.Background);
    }

    public void Add(Project project)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        project.Name = LocalizationProjectNames.ResourceManager
            .GetString(project.DescriptionNumber.ToString());
        Dispatcher.UIThread.Post(() => { GameScreenLogic.Add(project); }, DispatcherPriority.Background);
    }

    public void StartProject(int id)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.StartProject(id); }, DispatcherPriority.Background);
    }

    public void CompleteProject(int id)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.CompleteProject(id); }, DispatcherPriority.Background);
    }

    public void FailProject(int id)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.FailProject(id); }, DispatcherPriority.Background);
    }

    public void UpdateProjectProgress(int id, ProgressPoints points)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.UpdateProjectProgress(id, points); },
            DispatcherPriority.Background);
    }

    public void AddOpportunity(int number)
    {
        if (GameScreenLogic is null)
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
        Dispatcher.UIThread.Post(() => { GameScreenLogic.AddOpportunity(opportunity); },
            DispatcherPriority.Background);
    }

    public void RemoveOpportunity(int number)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.RemoveOpportunity(number); }, DispatcherPriority.Background);
    }

    public async Task ChangeCurrentStageAsync(GameStages stage, int time)
    {
        var stageName = LocalizationGameStages.ResourceManager.GetString(stage.ToString());
        if (GameScreenLogic is null || stageName is null)
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
            GameScreenLogic.GameStage = stage;
            GameScreenLogic.GameStageName = stageName;
            _time = time;
            _timer = Task.Run(ProcessTimerAsync);
        }, DispatcherPriority.Background);
    }

    public void ChangeOfficeState(int officeId, OfficeStates officeState)
    {
        if (GameScreenLogic is null)
        {
            return;
        }

        Dispatcher.UIThread.Post(() => { GameScreenLogic.SetOfficeState(officeId, officeState); },
            DispatcherPriority.Background);
    }
    
    private async Task ProcessTimerAsync()
    {
        for (; _time > -1; --_time)
        {
            if (GameScreenLogic is null)
            {
                return;
            }

            Dispatcher.UIThread.Post(() => { GameScreenLogic.Time = $"{_time / 60:D2}:{_time % 60:D2}"; },
                DispatcherPriority.Background);
            await Task.Delay(1000);
        }
    }
}