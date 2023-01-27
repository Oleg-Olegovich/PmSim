using Godot;
using Godot.Collections;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class EmailConfirmationScreen : BaseScreen
{
    private LineEdit _codeLine;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _label = (Label) GetChild(0).GetChild(0).GetChild(0);
        _codeLine = (LineEdit) GetChild(0).GetChild(1).GetChild(0);
        _buttons = new Array<Button>(GetChild(0).GetChild(2).GetChildren());
        _buttons[0].Connect("pressed", this, "ConfirmButtonPressed");
        _buttons[1].Connect("pressed", this, "BackButtonPressed");
    }

    public override void UpdateText()
    {
        _label.Text = Core.LanguageProvider.ConfirmEmail;
        _codeLine.PlaceholderText = Core.LanguageProvider.ConfirmationCode;
        _buttons[0].Text = Core.LanguageProvider.Confirm;
        _buttons[1].Text = Core.LanguageProvider.Back;
    }

    protected override void BackButtonPressed()
    {
        Core.ChangeScreen(Screens.SignUp);
    }
    
    private void ConfirmButtonPressed()
    {
        // Нужна проверка кода!
        Core.ChangeScreen(Screens.GamesList);
    }
}
