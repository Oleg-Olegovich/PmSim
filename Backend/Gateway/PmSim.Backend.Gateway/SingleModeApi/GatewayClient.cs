namespace PmSim.Backend.Gateway.SingleModeApi;

public class GatewayClient //: IGatewayClient
{
    /*
    private Game _game;

    public async Task<IsOkResponse> CreateAccountAsync(CreateAccountRequest request)
    {
        // Doesn't make sense in single player mode.
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<TokenResponse> GetAccountAsync(AuthorizationRequest request)
    {
        return new TokenResponse()
        {
            AccessToken = ""
        };
    }

    public async Task<GameModel> CreateNewGameAsync(CreateGameRequest request)
    {
        if (request.Mode < 0 || request.Mode > 2)
        {
            throw new ArgumentException("Invalid mode.");
        }

        var settings = request.IsDefaultSettings
            ? new GameOptions(request.Mode)
            : ConvertSettings(request.Mode, request.Settings);
        _game = new Game(request.Founder, 0, request.MaxPlayersNumber, request.BotsNumber, settings);
        return new GameModel()
        {
            Id = _game.Id
        };
    }

    /// <summary>
    /// A check is needed here: checking that the player is logged in
    /// and can connect to the game (does not play another one, etc.)
    /// </summary>
    public async Task<IsOkResponse> ConnectToGame(ActionRequest request)
    {
        await _game.ConnectAsync(request.PlayerId, await GetNameById(request.PlayerId));
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<GameModel> GetGameAsync(ActionRequest request)
    {
        return new GameModel()
        {
            Id = _game.Id
        };
    }

    public async Task<GameModel> GetGameAsync(string founder)
    {
        return new GameModel()
        {
            Id = _game.Id
        };
    }

    public async Task<GameStatusModel> GetGameStatusAsync(ActionRequest request)
    {
        if (_game == null)
        {
            throw new ArgumentException("Invalid request.");
        }

        GameStatusModel status;
        switch (_game.Stage)
        {
            case GameStages.Diplomacy:
                status = new DiplomacyGameStatusModel()
                {
                    Auctions = _game.Stage == GameStages.Diplomacy
                        ? await CreateAuctionModelsAsync(_game, request.PlayerId)
                        : null
                };
                break;
            case GameStages.IncidentDiscussion:
                status = new IncidentGameStatusModel()
                {
                    Incident = CreateModel<Incident, IncidentModel>(_game.CurrentIncident)
                };
                break;
            case GameStages.IncidentResolution:
                status = new IncidentGameStatusModel()
                {
                    Incident = CreateModel<Incident, IncidentResultModel>(_game.CurrentIncident)
                };
                break;
            case GameStages.IsOver:
                status = new FinishGameStatusModel()
                {
                    Winner = CreateModel<Player, ActorModel>(_game.Winner)
                };
                break;
            case GameStages.SummingUpResults:
            case GameStages.ChoosingBackground:
            case GameStages.Sprint:
            default:
                var player = _game.FindPlayerById(request.PlayerId);
                status = new GameStatusModel
                {
                    Actors = CreateModels<Player, ActorModel>(_game.Actors.Where(x => x != player).ToArray())
                };
                break;
        }

        status.Stage = _game.Stage;
        status.Time = _game.Time;
        status.Offices = CreateModels<Office, OfficeModel>(_game.Offices);
        status.MapNumber = _game.GameMap;
        status.Player = CreatePlayerModel(_game.FindPlayerById(request.PlayerId));
        return status;
    }

    public async Task<IsOkResponse> SkipMove(ActionRequest request)
    {
        await _game.SkipMoveAsync(request.PlayerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ExitGame(ActionRequest request)
    {
        await _game.GiveUpAsync(request.PlayerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SetBackgroundAsync(SetBackgroundRequest request)
    {
        await _game.ChooseBackgroundAsync(request.PlayerId, request.Profession);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> RentOfficeAsync(OfficeActionRequest request)
    {
        await _game.RentOfficeAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> CancelOfficeLeaseAsync(OfficeActionRequest request)
    {
        await _game.CancelOfficeLeaseAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> DismissAllEmployeesAsync(OfficeActionRequest request)
    {
        await _game.DismissAllEmployeesAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<EmployeeInfoResponse> ConductInterviewAsync(OfficeActionRequest request)
    {
        var employee = await _game.ConductInterviewAsync(request.PlayerId, request.OfficeNumber);
        return new EmployeeInfoResponse()
        {
            NameNumbers = employee.NameNumbers,
            DesiredSalary = employee.DesiredSalary,
            Programming = employee.Skills.Programming,
            Music = employee.Skills.Music,
            Design = employee.Skills.Design,
            Management = employee.Skills.Management,
            Creativity = employee.Skills.Creativity,
        };
    }

    public async Task<IsOkResponse> ProcessInterviewAsync(InterviewActionRequest request)
    {
        return new IsOkResponse()
        {
            IsOk = await _game.ProcessInterviewAsync(request.PlayerId, request.OfficeNumber, request.ProposedSalary)
        };
    }

    public async Task<IsOkResponse> HireTechSupportOfficerAsync(OfficeActionRequest request)
    {
        await _game.HireTechSupportOfficerAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> DismissTechSupportOfficerAsync(OfficeActionRequest request)
    {
        await _game.DismissTechSupportOfficerAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> UseOpportunityAsync(OpportunityActionRequest request)
    {
        await _game.UseOpportunityAsync(request.PlayerId, request.OpportunityNumber, request.TargetPlayer);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> AssignToWorkAsync(DevelopmentActionRequest request)
    {
        await _game.AssignToWorkAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.FeatureNumber,
            request.ProgressPointNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> AssignToInventProjectAsync(ExecutorActionRequest request)
    {
        await _game.AssignToInventProjectAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> AssignToMakeBackupAsync(FeaturesActionRequest request)
    {
        await _game.AssignToMakeBackupAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.FeatureNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> CancelTaskAsync(ExecutorActionRequest request)
    {
        await _game.CancelTaskAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> PutProjectUpForAuctionAsync(ProjectAuctionActionRequest request)
    {
        await _game.PutProjectUpForAuctionAsync(request.PlayerId, request.ProjectNumber, request.StartPrice);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ProposeProjectAsync(ProposeProjectActionRequest request)
    {
        await _game.ProposeProjectAsync(request.PlayerId, request.ProjectNumber, request.StartPrice,
            request.BuyerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> PutExecutorUpForAuctionAsync(ExecutorAuctionActionRequest request)
    {
        await _game.PutExecutorUpForAuctionAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.StartPrice);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ProposeExecutorAsync(ProposeExecutorActionRequest request)
    {
        await _game.ProposeExecutorAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.StartPrice, request.BuyerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> PutOpportunityUpForAuctionAsync(OpportunityAuctionActionRequest request)
    {
        await _game.PutOpportunityUpForAuctionAsync(request.PlayerId, request.OpportunityNumber,
            request.StartPrice);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ProposeOpportunityAsync(ProposeOpportunityActionRequest request)
    {
        await _game.ProposeOpportunityAsync(request.PlayerId, request.OpportunityNumber, request.StartPrice,
            request.BuyerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SendMessageAsync(SendMessageActionRequest request)
    {
        await _game.SendMessageAsync(request.PlayerId, request.RecipientId, request.Message);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SendMessageToEveryoneAsync(SendMessageToEveryoneActionRequest request)
    {
        await _game.SendMessageToEveryoneAsync(request.PlayerId, request.Message);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ParticipateInAuctionAsync(AuctionActionRequest request)
    {
        await _game.ParticipateInAuctionAsync(request.PlayerId, request.AuctionNumber, request.OfferSum);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SetIncidentActionAsync(IncidentActionRequest request)
    {
        await _game.MakeDecisionOnIncidentAsync(request.PlayerId, request.Donation);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<string> GetNameById(int playerId)
    {
        return _game.Founder;
    }

    private static GameOptions ConvertSettings(int mode, SettingsModel model)
        => new GameOptions(mode, model.ConnectionRealTime, model.ChoosingBackgroundRealTime,
            model.SprintRealTime, model.DiplomacyRealTime, model.IncidentRealTime, model.AuctionRealTime,
            model.SprintActionsNumbers, model.StartUpCapital, model.MapNumber);

    /// <summary>
    /// The universal method for converting a game model into a gateway contract.
    /// </summary>
    private static TContractType CreateModel<TModelType, TContractType>(TModelType model,
        MapperConfiguration custom = null)
    {
        var config = custom ?? new MapperConfiguration(cfg
            => cfg.CreateMap<TModelType, TContractType>());
        var mapper = new Mapper(config);
        return mapper.Map<TContractType>(model);
    }

    private static TContractType[] CreateModels<TModelType, TContractType>(IReadOnlyList<TModelType> models,
        MapperConfiguration custom = null)
    {
        var contracts = new TContractType[models.Count];
        for (var i = 0; i < models.Count; ++i)
        {
            contracts[i] = CreateModel<TModelType, TContractType>(models[i], custom);
        }

        return contracts;
    }

    private static PlayerModel CreatePlayerModel(Player player)
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Reward, RewardModel>();
            cfg.CreateMap<ProgressPoints, ProgressPointsModel>();
            cfg.CreateMap<Feature, FeatureModel>();
            cfg.CreateMap<Project, ProjectModel>();
            cfg.CreateMap<Player, PlayerModel>();
        });
        return CreateModel<Player, PlayerModel>(player, config);
    }

    private static async Task<AuctionModel[]> CreateAuctionModelsAsync(Game game, int playerId)
    {
        var gameAuctions = await game.RequestIncomingOffersAsync(playerId);
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Opportunity, OpportunityModel>();
            cfg.CreateMap<Reward, RewardModel>();
            cfg.CreateMap<ProgressPoints, ProgressPointsModel>();
            cfg.CreateMap<Feature, FeatureModel>();
            cfg.CreateMap<Project, ProjectModel>();
            cfg.CreateMap<SkillsPoints, SkillsPointsModel>();
            cfg.CreateMap<Employee, EmployeeModel>();
            cfg.CreateMap<IAuctionLot, ILotModel>();
        });
        var auctions = CreateModels<Auction, AuctionModel>(gameAuctions, config);
        for (var i = 0; i < auctions.Length; ++i)
        {
            if (!(gameAuctions[i].Lot is Employee gameEmployee))
            {
                continue;
            }

            var gameTask = gameEmployee.CurrentTask;
            var employee = auctions[i].Lot as EmployeeModel;
            Debug.Assert(employee != null, nameof(employee) + " is null");
            employee.TaskShortDescription = new EmployeeTaskModel
            {
                PlayerId = gameTask.Player.Id
            };
            switch (gameEmployee.CurrentTask)
            {
                case EmployeeBackUpTask backUpTask:
                    employee.TaskShortDescription.Type = EmployeeTaskModelTypes.BackUpMaking;
                    employee.TaskShortDescription.TaskObject = backUpTask.OfficeNumber;
                    continue;
                case EmployeeWorkTask workTask:
                {
                    employee.TaskShortDescription.Type = EmployeeTaskModelTypes.BackUpMaking;
                    var playerProjects = game.FindPlayerById(playerId).Projects;
                    if (workTask.Project is Project project)
                    {
                        employee.TaskShortDescription.TaskObject = playerProjects.FindIndex(x => x == project);
                    }

                    employee.TaskShortDescription.TaskObject =
                        playerProjects.FindIndex(x => x.Features.Contains(workTask.Project));
                    continue;
                }
                default:
                    employee.TaskShortDescription.Type = EmployeeTaskModelTypes.Inventing;
                    employee.TaskShortDescription.TaskObject = -1;
                    break;
            }
        }

        return auctions;
    }
    //*/
}