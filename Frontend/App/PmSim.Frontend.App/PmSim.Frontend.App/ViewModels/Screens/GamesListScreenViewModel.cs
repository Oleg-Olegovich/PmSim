using System.Reactive;
using System.Threading.Tasks;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Exceptions;
using PmSim.Shared.Contracts.Game;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GamesListScreenViewModel : BasicScreenViewModel
{
    private readonly MultiPlayerClient _client;

    private GameModel[]? _games;
    public GameModel[]? Games
    {
        get => _games;
        set => this.RaiseAndSetIfChanged(ref _games, value);
    }

    private GameModel? _selectedGame;

    public GameModel? SelectedGame
    {
        get => _selectedGame;
        set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
    }

    public ReactiveCommand<Unit, Unit> NewGameCommand { get; }
    
    public ReactiveCommand<Unit, Unit> ConnectCommand { get; }
    
    public ReactiveCommand<Unit, Unit> SubscriptionPurchaseCommand { get; }

    public GamesListScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, MultiPlayerClient client) 
        : base(baseWindow, previous)
    {
        NewGameCommand = ReactiveCommand.Create(ProcessNewGameCommand);
        ConnectCommand = ReactiveCommand.Create(ProcessConnectCommand);
        SubscriptionPurchaseCommand = ReactiveCommand.Create(ProcessSubscriptionPurchase);
        _client = client;
        Task.Run(UpdateGamesList);
    }

    private void ProcessNewGameCommand()
        => BaseWindow.Content = new GameOptionsScreenViewModel(BaseWindow, this, _client, false);
    
    private void ProcessConnectCommand()
    {
        if (SelectedGame is not null)
        {
            try
            {
                _client.ConnectToGame(SelectedGame.Id);
                BaseWindow.Content = new GameScreenViewModel(BaseWindow, this, _client);
            }
            catch (PmSimException exception)
            {
                BaseWindow.Content = new ErrorScreenViewModel(BaseWindow, this, exception.Message);
            }
        }
    }
    
    private void ProcessSubscriptionPurchase()
        => BaseWindow.Content = new SubscriptionPurchaseScreenViewModel(BaseWindow, this, this);
    
    private async Task UpdateGamesList()
    {
        while (BaseWindow.Content == this)
        {
            Games = _client.GetActiveGames();
            await Task.Delay(1000);
        }
    }
}