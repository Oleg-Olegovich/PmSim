using Godot;
using System;
using Godot.Collections;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class SingleSignInScreen : BaseScreen
{
    private LineEdit _nameLine;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _label = (Label) FindNode("HeaderLabel");
        _nameLine = (LineEdit) FindNode("NameLine");
        _buttons = new Array<Button>(FindNode("ButtonsContainer").GetChildren());
        _buttons[0].Connect("pressed", this, "NextButtonPressed");
        _buttons[1].Connect("pressed", this, "BackButtonPressed");
    }

    public override void UpdateText()
    {
        _label.Text = Core.LanguageProvider.InputName;
        _nameLine.PlaceholderText = Core.LanguageProvider.Name;
        _buttons[0].Text = Core.LanguageProvider.Next;
        _buttons[1].Text = Core.LanguageProvider.Back;
    }

    private void NextButtonPressed()
    {
        Core.IsSingleGame = true;
        Core.Domain.PlayerName = _nameLine.Text;
        Core.ChangeScreen(Screens.GameSettings);
    }
}
