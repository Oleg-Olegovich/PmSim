using System.Reactive;
using PmSim.Frontend.App.ViewModels.Screens;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class GameMap0ViewModel : BasicGameMapViewModel
{
    public ReactiveCommand<int, Unit> OfficeCommand { get; }
    
    public ReactiveCommand<Unit, Unit> AuctionCommand { get; }
    
    public GameMap0ViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen)
    {
        OfficeCommand = ReactiveCommand.Create<int>(ProcessOfficeClick);
        AuctionCommand = ReactiveCommand.Create(ProcessAuctionClick);
    }

    private void ProcessOfficeClick(int officeNumber) 
        => GameScreen.ShowOffice(officeNumber);
    
    private void ProcessAuctionClick()
        => GameScreen.ShowAuctionHouseMenu();
}