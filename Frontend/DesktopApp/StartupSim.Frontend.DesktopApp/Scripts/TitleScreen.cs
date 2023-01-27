using Godot;
using Godot.Collections;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class TitleScreen : BaseScreen
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _buttons = new Array<Button>(FindNode("ButtonsContainer").GetChildren());
        _buttons[0].Connect("pressed", this, "SinglePlayerButtonPressed");
        _buttons[1].Connect("pressed", this, "MultiPlayerButtonPressed");
        _buttons[2].Connect("pressed", this, "OptionsButtonPressed");
        _buttons[3].Connect("pressed", this, "ExitButtonPressed");
    }

    public override void UpdateText()
    {
        _buttons[0].Text = Core.LanguageProvider.Singleplayer;
        _buttons[1].Text = Core.LanguageProvider.Multiplayer;
        _buttons[2].Text = Core.LanguageProvider.Options;
        _buttons[3].Text = Core.LanguageProvider.Exit;
    }

    public void SinglePlayerButtonPressed()
    {
        Core.ChangeScreen(Screens.SingleSignIn);
    }
    
    public void MultiPlayerButtonPressed()
    {
        Core.ChangeScreen(Screens.SignIn);
    }
    
    public void OptionsButtonPressed()
    {
        Core.ChangeScreen(Screens.Options);
    }

    public void ExitButtonPressed()
    {
        GetTree().Quit();
    }
}