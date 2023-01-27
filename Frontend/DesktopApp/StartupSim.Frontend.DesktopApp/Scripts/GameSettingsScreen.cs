using Godot;
using Godot.Collections;

public class GameSettingsScreen : BaseScreen
{
    private int _gameMapNumber = 0;

    private Array<VBoxContainer> _settings;

    private CheckBox _defaultCheckBox;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _label = (Label)FindNode("SettingsLabel");
        _settings = new Array<VBoxContainer>(FindNode("GridContainer").GetChildren());
        _buttons = new Array<Button>(FindNode("ButtonsContainer").GetChildren());
        _defaultCheckBox = (CheckBox)FindNode("DefaultCheckButton");
        _buttons[0].Connect("pressed", this, "StartButtonPressed");
        _buttons[1].Connect("pressed", this, "BackButtonPressed");
        _defaultCheckBox.Connect("pressed", this, "DefaultCheckBoxPressed");
        _defaultCheckBox.Pressed = true;
        DefaultCheckBoxPressed();
    }
    
    public override void UpdateText()
    {
        _buttons[0].Text = Core.LanguageProvider.Start;
        _buttons[1].Text = Core.LanguageProvider.Back;
        _label.Text = Core.LanguageProvider.Options;
        _defaultCheckBox.Text = Core.LanguageProvider.Default;
        ((Label) _settings[0].GetChild(0)).Text = Core.LanguageProvider.ConnectionRealTime;
        ((Label) _settings[1].GetChild(0)).Text = Core.LanguageProvider.ChoosingBackgroundRealTime;
        ((Label) _settings[2].GetChild(0)).Text = Core.LanguageProvider.SprintRealTime;
        ((Label) _settings[3].GetChild(0)).Text = Core.LanguageProvider.DiplomacyRealTime;
        ((Label) _settings[4].GetChild(0)).Text = Core.LanguageProvider.IncidentRealTime;
        ((Label) _settings[5].GetChild(0)).Text = Core.LanguageProvider.SprintActionsNumbers;
        ((Label) _settings[6].GetChild(0)).Text = Core.LanguageProvider.AuctionRealTime;
        ((Label) _settings[7].GetChild(0)).Text = Core.LanguageProvider.StartUpCapital;
    }
    
    private void StartButtonPressed()
    {
        Core.StartGame(_gameMapNumber);
    }

    private void DefaultCheckBoxPressed()
    {
        foreach (var vbox in _settings)
        {
            ((SpinBox) vbox.GetChild(1)).Editable = !_defaultCheckBox.Pressed;
        }
    }
}