using Godot;
using Godot.Collections;

public abstract class GameMap : Sprite
{
    private Array<GameMapButton> _gameButtons;
    
    public GameScreen GameScreen
    {
        set
        {
            InitializeButtons();
            var index = -1;
            foreach (var button in _gameButtons)
            {
                button.GameScreen = value;
                if (button is OfficeButton officeButton)
                {
                    officeButton.Index = ++index;
                }
            }
        }
    }

    public override void _Ready()
    {
        InitializeButtons();
    }

    public void SetButtonDisableIs(bool isDisable)
    {
        foreach (var button in _gameButtons)
        {
            button.Disabled = isDisable;
        }
    }
    
    private void InitializeButtons()
    {
        if (_gameButtons == null)
        {
            _gameButtons = new Array<GameMapButton>(GetChildren());
        }
    }
}