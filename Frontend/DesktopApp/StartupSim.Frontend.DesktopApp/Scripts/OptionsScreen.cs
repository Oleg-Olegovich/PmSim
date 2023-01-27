using Godot;
using Godot.Collections;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class OptionsScreen : BaseScreen
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _label = (Label)GetChild(0).GetChild(0).GetChild(0);
        _buttons = new Array<Button>(GetChild(0).GetChild(1).GetChildren());
        _buttons[0].Connect("item_selected", this, "LanguageSelected");
        _buttons[1].Connect("pressed", this, "BackButtonPressed");
        if (!(_buttons[0] is OptionButton optionButton))
        {
            return;
        }
        optionButton.AddItem("English");
        optionButton.AddItem("Русский");
    }
    
    public void LanguageSelected(int index) 
    {
        Core.ChangeLanguage((Languages)index);
        UpdateText();
    }

    public override void UpdateText()
    {
        _buttons[1].Text = Core.LanguageProvider.Back;
        _label.Text = Core.LanguageProvider.Options;
    }
}
