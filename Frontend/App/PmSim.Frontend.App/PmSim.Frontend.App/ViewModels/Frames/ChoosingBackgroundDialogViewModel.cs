using System.Reactive;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Frontend.Client.Api;
using PmSim.Shared.Contracts.Enums;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class ChoosingBackgroundDialogViewModel : BasicFrameViewModel
{
    private int _selectedBackground;
    
    public int SelectedBackground
    {
        get => _selectedBackground;
        set => this.RaiseAndSetIfChanged(ref _selectedBackground, value);
    }
    
    public string[] Backgrounds { get; }
    
    public ReactiveCommand<Unit, Unit> ConfirmationCommand { get; }
    
    public ChoosingBackgroundDialogViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen)
    {
        Backgrounds = IPmSimClient.GetBackgrounds();
        ConfirmationCommand = ReactiveCommand.Create(Confirm);
    }

    private void Confirm()
        => GameScreen.ChooseBackground((Professions)SelectedBackground);
}