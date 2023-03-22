using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Dto;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GamesListScreenViewModel : BasicScreenViewModel
{
    private readonly PmSimClient _client;

    private Game[]? _games;
    public Game[]? Games
    {
        get => _games;
        set => this.RaiseAndSetIfChanged(ref _games, value);
    }

    private Game? _selectedGame;

    public Game? SelectedGame
    {
        get => _selectedGame;
        set => this.RaiseAndSetIfChanged(ref _selectedGame, value);
    }

    public ReactiveCommand<Unit, Unit> NewGameCommand { get; }
    
    public ReactiveCommand<Unit, Unit> ConnectCommand { get; }

    public GamesListScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, PmSimClient client) 
        : base(baseWindow, previous)
    {
        NewGameCommand = ReactiveCommand.Create(ProcessNewGameCommand);
        ConnectCommand = ReactiveCommand.Create(ProcessConnectCommand);
        _client = client;
        Task.Run(UpdateGamesList);
    }

    private void ProcessNewGameCommand()
    {
        
    }
    
    private void ProcessConnectCommand()
    {
        
    }
    
    private async Task UpdateGamesList()
    {
        while (BaseWindow.Content == this)
        {
            Games = _client.GetActiveGames();
            await Task.Delay(1000);
        }
    }
}