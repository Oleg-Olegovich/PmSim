namespace PmSim.Backend.Gateway.MultiplayerModeApi;

public class GatewayClient //: IGatewayClient
{
    /*
    private Uri _baseUrl;

    private List<Game> _games = new();

    /// <summary>
    /// Key = player name, value - token (is generated when player connects to the game).
    /// </summary>
    private Dictionary<string, int> _players = new Dictionary<string, int>();

    public GatewayClient(IOptions<GatewayClientOptions> options)
    {
        _baseUrl = options.Value.BaseUri;
    }

    public async Task<IsOkResponse> CreateAccountAsync(CreateAccountRequest request)
    {
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
            ? new Options(request.Mode)
            : ConvertSettings(request.Mode, request.Settings);
        var game = new Game(request.Founder, _games.Count, request.MaxPlayersNumber, request.BotsNumber, settings);
        _games.Add(game);
        return new GameModel()
        {
            Id = game.Id
        };
    }

    /// <summary>
    /// A check is needed here: checking that the player is logged in
    /// and can connect to the game (does not play another one, etc.)
    /// </summary>
    public async Task<IsOkResponse> ConnectToGame(ActionRequest request)
    {
        await _games[request.GameId].ConnectAsync(request.PlayerId, await GetNameById(request.PlayerId));
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<GameModel> GetGameAsync(ActionRequest request)
    {
        return new GameModel()
        {
            Id = _games[request.GameId].Id
        };
    }

    public async Task<GameModel> GetGameAsync(string founder)
    {
        return new GameModel()
        {
            Id = _games.FindIndex(x => x.Founder == founder)
        };
    }

    public async Task<GameStatusModel> GetGameStatusAsync(ActionRequest request)
    {
        if (_games[request.GameId] == null)
        {
            throw new ArgumentException("Invalid request.");
        }

        GameStatusModel status;
        switch (_games[request.GameId].Stage)
        {
            case GameStages.Diplomacy:
                status = new DiplomacyGameStatusModel()
                {
                    Auctions = _games[request.GameId].Stage == GameStages.Diplomacy
                        ? await CreateAuctionModelsAsync(_games[request.GameId], request.PlayerId)
                        : null
                };
                break;
            case GameStages.IncidentDiscussion:
                status = new IncidentGameStatusModel()
                {
                    Incident = CreateModel<Incident, IncidentModel>(_games[request.GameId].CurrentIncident)
                };
                break;
            case GameStages.IncidentResolution:
                status = new IncidentGameStatusModel()
                {
                    Incident = CreateModel<Incident, IncidentResultModel>(_games[request.GameId].CurrentIncident)
                };
                break;
            case GameStages.IsOver:
                status = new FinishGameStatusModel()
                {
                    Winner = CreateModel<Player, ActorModel>(_games[request.GameId].Winner)
                };
                break;
            case GameStages.SummingUpResults:
            case GameStages.NotStarted:
            case GameStages.Connection:
            case GameStages.ChoosingBackground:
            case GameStages.Sprint:
            default:
                status = new GameStatusModel
                {
                    Actors = CreateModels<Player, ActorModel>(_games[request.GameId].Actors)
                };
                break;
        }

        status.Stage = _games[request.GameId].Stage;
        status.Time = _games[request.GameId].Time;
        status.Offices = CreateModels<Office, OfficeModel>(_games[request.GameId].Offices);
        status.MapNumber = _games[request.GameId].GameMap;
        status.Player = CreatePlayerModel(_games[request.GameId].FindPlayerById(request.PlayerId));
        return status;
    }

    public async Task<IsOkResponse> SkipMove(ActionRequest request)
    {
        await _games[request.GameId].SkipMoveAsync(request.PlayerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ExitGame(ActionRequest request)
    {
        await _games[request.GameId].GiveUpAsync(request.PlayerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SetBackgroundAsync(SetBackgroundRequest request)
    {
        await _games[request.GameId].ChooseBackgroundAsync(request.PlayerId, request.Profession);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> RentOfficeAsync(OfficeActionRequest request)
    {
        await _games[request.GameId].RentOfficeAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> CancelOfficeLeaseAsync(OfficeActionRequest request)
    {
        await _games[request.GameId].CancelOfficeLeaseAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> DismissAllEmployeesAsync(OfficeActionRequest request)
    {
        await _games[request.GameId].DismissAllEmployeesAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<EmployeeInfoResponse> ConductInterviewAsync(OfficeActionRequest request)
    {
        var employee = await _games[request.GameId].ConductInterviewAsync(request.PlayerId, request.OfficeNumber);
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
            IsOk = await _games[request.GameId].ProcessInterviewAsync(request.PlayerId, request.OfficeNumber, request.ProposedSalary)
        };
    }

    public async Task<IsOkResponse> HireTechSupportOfficerAsync(OfficeActionRequest request)
    {
        await _games[request.GameId].HireTechSupportOfficerAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> DismissTechSupportOfficerAsync(OfficeActionRequest request)
    {
        await _games[request.GameId].DismissTechSupportOfficerAsync(request.PlayerId, request.OfficeNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> UseOpportunityAsync(OpportunityActionRequest request)
    {
        await _games[request.GameId].UseOpportunityAsync(request.PlayerId, request.OpportunityNumber, request.TargetPlayer);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> AssignToWorkAsync(DevelopmentActionRequest request)
    {
        await _games[request.GameId].AssignToWorkAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.FeatureNumber,
            request.ProgressPointNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> AssignToInventProjectAsync(ExecutorActionRequest request)
    {
        await _games[request.GameId].AssignToInventProjectAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> AssignToMakeBackupAsync(FeaturesActionRequest request)
    {
        await _games[request.GameId].AssignToMakeBackupAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.FeatureNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> CancelTaskAsync(ExecutorActionRequest request)
    {
        await _games[request.GameId].CancelTaskAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> PutProjectUpForAuctionAsync(ProjectAuctionActionRequest request)
    {
        await _games[request.GameId].PutProjectUpForAuctionAsync(request.PlayerId, request.ProjectNumber, request.StartPrice);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ProposeProjectAsync(ProposeProjectActionRequest request)
    {
        await _games[request.GameId].ProposeProjectAsync(request.PlayerId, request.ProjectNumber, request.StartPrice,
            request.BuyerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> PutExecutorUpForAuctionAsync(ExecutorAuctionActionRequest request)
    {
        await _games[request.GameId].PutExecutorUpForAuctionAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.StartPrice);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ProposeExecutorAsync(ProposeExecutorActionRequest request)
    {
        await _games[request.GameId].ProposeExecutorAsync(request.PlayerId, request.OfficeNumber, request.ExecutorNumber,
            request.StartPrice, request.BuyerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> PutOpportunityUpForAuctionAsync(OpportunityAuctionActionRequest request)
    {
        await _games[request.GameId].PutOpportunityUpForAuctionAsync(request.PlayerId, request.OpportunityNumber,
            request.StartPrice);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ProposeOpportunityAsync(ProposeOpportunityActionRequest request)
    {
        await _games[request.GameId].ProposeOpportunityAsync(request.PlayerId, request.OpportunityNumber, request.StartPrice,
            request.BuyerId);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SendMessageAsync(SendMessageActionRequest request)
    {
        await _games[request.GameId].SendMessageAsync(request.PlayerId, request.RecipientId, request.Message);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SendMessageToEveryoneAsync(SendMessageToEveryoneActionRequest request)
    {
        await _games[request.GameId].SendMessageToEveryoneAsync(request.PlayerId, request.Message);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> ParticipateInAuctionAsync(AuctionActionRequest request)
    {
        await _games[request.GameId].ParticipateInAuctionAsync(request.PlayerId, request.AuctionNumber, request.OfferSum);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<IsOkResponse> SetIncidentActionAsync(IncidentActionRequest request)
    {
        await _games[request.GameId].MakeDecisionOnIncidentAsync(request.PlayerId, request.Donation);
        return new IsOkResponse()
        {
            IsOk = true
        };
    }

    public async Task<string> GetNameById(int playerId)
    {
        var player = _players.FirstOrDefault(x => x.Value == playerId);
        return player.Key ?? "";
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