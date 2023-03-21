using System.Collections.Generic;
using System.Threading.Tasks;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using PmSim.Frontend.Client.Dto;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class GamesListScreenViewModel : BasicScreenViewModel
{
    private readonly PmSimClient _client;

    private List<Game>? _games;
    public List<Game>? Games
    {
        get => _games;
        set => this.RaiseAndSetIfChanged(ref _games, value);
    }

    public GamesListScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous, PmSimClient client) 
        : base(baseWindow, previous)
    {
        _client = client;
        Task.Run(UpdateGamesList);
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