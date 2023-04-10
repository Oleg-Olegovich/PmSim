using System.Reactive;
using PmSim.Frontend.App.ViewModels.Screens;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class AuctionMenuViewModel : BasicFrameViewModel
{
    private int _bet = 1;

    public int Bet
    {
        get => _bet;
        set => this.RaiseAndSetIfChanged(ref _bet, value);
    }

    public ReactiveCommand<Unit, Unit> ConfirmCommand { get; }
    
    public ReactiveCommand<Unit, Unit> CreateCommand { get; }
    
    public AuctionMenuViewModel(GameScreenViewModel gameScreen) : base(gameScreen)
    {
        ConfirmCommand = ReactiveCommand.Create(ProcessConfirmation);
        CreateCommand = ReactiveCommand.Create(ProcessCreation);
    }

    private void ProcessConfirmation()
    {
        
    }

    private void ProcessCreation()
    {
        GameScreen.MainAreaContent = new AuctionCreationViewModel(GameScreen);
        GameScreen.CurrentTabIndex = 0;
    }
}