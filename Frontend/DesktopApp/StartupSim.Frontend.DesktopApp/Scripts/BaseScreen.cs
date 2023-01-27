using Godot;
using Godot.Collections;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public abstract class BaseScreen : Node
{
    protected Array<Button> _buttons;
    protected Label _label;
    
    public Main Core { get; set; }

    public abstract void UpdateText();
    
    protected virtual void BackButtonPressed()
    {
        Core.ChangeScreen(Screens.Title);
    }

    protected void ShowAcceptDialog(string header, string message, string hideMethod = "")
    {
        var dialog = new AcceptDialog();
        AddChild(dialog);
        dialog.Theme = (Theme) GD.Load("res://Resources/calibri_bold_theme.tres");
        dialog.WindowTitle = header;
        dialog.DialogText = message;
        dialog.PopupCentered();
        dialog.Connect("popup_hide", this, hideMethod);
    }

    protected void ShowConfirmationDialog(string header, string message, string confirmMethod, string hideMethod = "")
    {
        var dialog = new ConfirmationDialog();
        AddChild(dialog);
        dialog.Theme = (Theme) GD.Load("res://Resources/calibri_bold_theme.tres");
        dialog.WindowTitle = header;
        dialog.DialogText = message;
        dialog.PopupCentered();
        dialog.Connect("confirmed", this, confirmMethod);
        dialog.Connect("popup_hide", this, hideMethod);
    }
}