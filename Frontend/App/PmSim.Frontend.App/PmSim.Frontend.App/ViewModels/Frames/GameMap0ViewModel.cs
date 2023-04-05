using System.Reactive;
using PmSim.Frontend.App.ViewModels.Screens;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class GameMap0ViewModel : BasicGameMapViewModel
{
    public ReactiveCommand<string, Unit> OfficeCommand { get; }
    
    public ReactiveCommand<Unit, Unit> AuctionCommand { get; }
    
    public GameMap0ViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen)
    {
        OfficeCommand = ReactiveCommand.Create<string>(ProcessOfficeClick);
        AuctionCommand = ReactiveCommand.Create(ProcessAuctionClick);
    }

    private void ProcessOfficeClick(string officeId) 
        => GameScreen.ShowOffice(int.Parse(officeId));
    
    private void ProcessAuctionClick()
        => GameScreen.ShowAuctionHouseMenu();
}