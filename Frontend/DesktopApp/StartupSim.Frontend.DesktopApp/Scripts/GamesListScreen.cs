using Godot;
using Godot.Collections;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class GamesListScreen : BaseScreen
{
    private ItemList _gamesList;
    
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _label = (Label) FindNode("GameListLabel");
        _buttons = new Array<Button>(FindNode("ButtonsContainer").GetChildren());
        _buttons[0].Connect("pressed", this, "NewGameButtonPressed");
        _buttons[1].Connect("pressed", this, "ConnectButtonPressed");
        _buttons[2].Connect("pressed", this, "BackButtonPressed");
        _gamesList = (ItemList)FindNode("GamesItemList");
        
        // Temporary:
        UpdateGamesList();
    }
    
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        //UpdateGamesList();
    }
    
    public override void UpdateText()
    {
        _label.Text = Core.LanguageProvider.GamesList;
        _buttons[0].Text = Core.LanguageProvider.NewGame;
        _buttons[1].Text = Core.LanguageProvider.Connect;
        _buttons[2].Text = Core.LanguageProvider.Back;
    }
    
    private void NewGameButtonPressed()
    {
        Core.ChangeScreen(Screens.GameSettings);
        Core.IsSingleGame = false;
    }
    
    private void ConnectButtonPressed()
    {
        ShowAcceptDialog("Under-construction!", "Under-construction!");
    }

    private void UpdateGamesList()
    {
        // This is the code for the demo version.
        var image = new Image();
        image.Load("res://Resources/russian_flag.png");
        var texture = new ImageTexture();
        texture.CreateFromImage(image);
        _gamesList.AddItem("Партия для русских", texture);
        image.Load("res://Resources/british_flag.png");
        var texture2 = new ImageTexture();
        texture2.CreateFromImage(image);
        _gamesList.AddItem("A game for the Englishmen", texture2);
        _gamesList.RectMinSize = _gamesList.GetFont("calibri_bold.tres")
            .GetStringSize("A game for the EnglishmenA game for the EnglishmenA game for the EnglishmenA game for the Englishmen");
    }
}
