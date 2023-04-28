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

    public GamesListScreenViewModel(MainViewModel mainView, BasicScreenViewModel previous, MultiPlayerClient client) 
        : base(mainView, previous)
    {
        NewGameCommand = ReactiveCommand.Create(ProcessNewGameCommand);
        ConnectCommand = ReactiveCommand.Create(ProcessConnectCommand);
        SubscriptionPurchaseCommand = ReactiveCommand.Create(ProcessSubscriptionPurchase);
        _client = client;
        Task.Run(UpdateGamesList);
    }

    private void ProcessNewGameCommand()
        => MainView.Content = new GameOptionsScreenViewModel(MainView, this, _client, false);
    
    private void ProcessConnectCommand()
    {
        if (SelectedGame is not null)
        {
            try
            {
                _client.ConnectToGame(SelectedGame.Id);
                MainView.Content = new GameScreenViewModel(MainView, this, _client);
            }
            catch (PmSimException exception)
            {
                MainView.Content = new ErrorScreenViewModel(MainView, this, exception.Message);
            }
        }
    }
    
    private void ProcessSubscriptionPurchase()
        => MainView.Content = new SubscriptionPurchaseScreenViewModel(MainView, this, this);
    
    private async Task UpdateGamesList()
    {
        while (MainView.Content == this)
        {
            Games = _client.GetActiveGames();
            await Task.Delay(1000);
        }
    }
}