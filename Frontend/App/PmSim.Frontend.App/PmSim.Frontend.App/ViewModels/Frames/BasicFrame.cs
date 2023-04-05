using System.Reactive;
using PmSim.Frontend.App.ViewModels.Screens;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public abstract class BasicFrame : ViewModelBase
{
    protected readonly GameScreenViewModel GameScreen;

    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
    
    protected BasicFrame(GameScreenViewModel gameScreen)
    {
        GameScreen = gameScreen;
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    private void Cancel()
        => GameScreen.ShowMapMenu();
}