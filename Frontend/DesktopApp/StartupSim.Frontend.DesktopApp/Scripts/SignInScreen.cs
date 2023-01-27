using Godot;
using Godot.Collections;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class SignInScreen : BaseScreen
{
    private Array<LineEdit> _lines;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _label = (Label)GetChild(0).GetChild(0).GetChild(0);
        _lines = new Array<LineEdit>(GetChild(0).GetChild(1).GetChildren());
        _buttons = new Array<Button>(GetChild(0).GetChild(2).GetChildren());
        _buttons[0].Connect("pressed", this, "SignInButtonPressed");
        _buttons[1].Connect("pressed", this, "SignUpButtonPressed");
        _buttons[2].Connect("pressed", this, "BackButtonPressed");
    }

    public override void UpdateText()
    {
        _label.Text = Core.LanguageProvider.SignIn;
        _lines[0].PlaceholderText = Core.LanguageProvider.Login;
        _lines[1].PlaceholderText = Core.LanguageProvider.Password;
        // Spaces are a crutch so that the English "password" is displayed in full.
        _buttons[0].Text = "   " + Core.LanguageProvider.SignIn + "   ";
        _buttons[1].Text = Core.LanguageProvider.SignUp;
        _buttons[2].Text = Core.LanguageProvider.Back;
    }
    
    public void SignInButtonPressed()
    {
        ShowAcceptDialog("Error", "The server is unavailable, try again later.");
        return;
        Core.IsSingleGame = false;
        // Here it is necessary to create a multi Domain.
        Core.Domain.PlayerName = _lines[0].Text;
        Core.ChangeScreen(Screens.GamesList);
    }
    
    public void SignUpButtonPressed()
    {
        Core.ChangeScreen(Screens.SignUp);
    }
}
