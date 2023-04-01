using PmSim.Frontend.App.ViewModels.Screens;

namespace PmSim.Frontend.App.ViewModels.Frames;

public abstract class BasicFrame : ViewModelBase
{
    protected GameScreenViewModel _gameScreen;

    protected BasicFrame(GameScreenViewModel gameScreen) 
        => _gameScreen = gameScreen;
}