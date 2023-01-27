using System;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using StartupSim.Backend.GameEngine.Enums;
using StartupSim.Backend.Gateway.Contracts.Actions;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;
using StartupSim.Frontend.DesktopApp.Scripts.Enums;
using Environment = System.Environment;

public class GameScreen : BaseScreen
{
    private ItemList _playersItemList;
    private GameMap _gameMap;
    private Label _playersLabel, _timeLabel, _stageLabel;
    private RichTextLabel _infoLabel;
    private int _selectedOfficeNumber;
    private GameMapContainer _mapContainer;
    private PanelContainer _framesContainer;
    private string[] _stageLabels;
    private const float Ratio = 13f / 24;

    public int GameMapNumber { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        GetTree().SetAutoAcceptQuit(false);
        _mapContainer = (GameMapContainer)FindNode("GameMapContainer");
        _framesContainer = (PanelContainer)FindNode("FramesContainer");
        _infoLabel = (RichTextLabel)FindNode("InfoLabel");
        _timeLabel = (Label)FindNode("TimeLabel");
        _playersLabel = (Label)FindNode("PlayersLabel");
        _stageLabel = (Label)FindNode("StageLabel");
        _buttons = new Array<Button>
        {
            (Button)FindNode("GiveUpButton"),
            (Button)FindNode("SkipButton")
        };
        _buttons[0].Connect("pressed", this, "BackButtonPressed");
        _buttons[1].Connect("pressed", this, "SkipButtonPressed");
        AddMap();
        AddPlayers();
        CalculateRectMinSize();
        ShowWaitingMenu();
        _stageLabels = Core.LanguageProvider.GameStages;
    }

    public override void _Process(float delta)
    {
        try
        {
            Core.Domain.UpdateStatusAsync();
            _timeLabel.Text = $"{Core.Domain.Time / 60:D2}:{Core.Domain.Time % 60:D2}";
            UpdateInfoLabels();
            OS.WindowSize = new Vector2(OS.WindowSize.x, OS.WindowSize.x * Ratio);
        }
        catch (Exception e)
        {
            GD.Print(e);
        }
    }

    public override void UpdateText()
    {
        _playersLabel.Text = Core.LanguageProvider.Players;
        _buttons[0].Text = Core.LanguageProvider.GiveUp;
        _buttons[1].Text = Core.LanguageProvider.Skip;
        UpdateInfoLabels();
    }

    public void MakeAction(ActionTypes action, int additionalArgument = 0)
    {
        try
        {
            switch (action)
            {
                case ActionTypes.Auction:
                    AuctionAction();
                    break;
                case ActionTypes.Office:
                    OfficeAction(additionalArgument);
                    break;
                default:
                    throw new Exception("Invalid action type!");
            }
        }
        catch (Exception exception)
        {
            ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = true;
            ShowAcceptDialog("Error!", exception.Message, "ExitMenu");
        }
    }

    private void AddMap()
    {
        var mapPath = "res://Scenes/GameMaps/Map_" + GameMapNumber + ".tscn";
        _gameMap = (GameMap)((PackedScene)GD.Load(mapPath)).Instance();
        _gameMap.GameScreen = this;
        _mapContainer.AddMap(_gameMap);
    }

    private void AddPlayers()
    {
        _playersItemList = (ItemList)FindNode("PlayersItemList");
        _playersItemList.AddItem("You");
        _playersItemList = (ItemList)FindNode("PlayersItemList");
        foreach (var actor in Core.Domain.Actors)
        {
            _playersItemList.AddItem(actor.Name);
        }
        /*
        var image = new Image();
        image.Load("res://icon.png");
        var texture = new ImageTexture();
        texture.CreateFromImage(image);
        _playersItemList.AddItem("You", texture);
        _playersItemList.AddItem("Player 0", texture);
        _playersItemList.AddItem("Player 1", texture);
        _playersItemList.AddItem("PlayerPlayerPlayerLongName", texture);
        _playersItemList.AddItem("Bot", texture);
        _playersItemList.RectMinSize = _playersItemList.GetFont("calibri_bold.tres")
            .GetStringSize(
                "Player 0Player 0Player 0Player 0Player 0PlayerPlayerPlayerLongNamePlayerPlayerPlayerLongName");
        */
    }

    private void UpdateInfoLabels()
    {
        _stageLabel.Text = _stageLabels[Core.Domain.GameStageLabel];

        var actions = Core.Domain.PlayerActor.ActionsNumber;
        var money = Core.Domain.PlayerActor.Money;
        var offices = Core.Domain.Offices;
        var projects = Core.Domain.PlayerActor.Projects.Length;
        var employees = Core.Domain.Employees;

        _infoLabel.Clear();
        _infoLabel.AppendBbcode("[right][img]res://Resources/Images/actions_icon.png[/img] " + actions
            + " [img]res://Resources/Images/money_icon.png[/img] " + money
            + " [img]res://Resources/Images/offices_icon.png[/img] " + offices
            + " [img]res://Resources/Images/projects_icon.png[/img] " + projects
            + " [img]res://Resources/Images/employees_icon.png[/img] " + employees + "[/right]");
    }

    private void CalculateRectMinSize()
    {
        var scrollContainer = (Container)FindNode("ScrollContainer");
        scrollContainer.RectMinSize = new Vector2(0.15F * 1920, 1080);
        var parametersContainer = (Container)FindNode("ParametersHBoxContainer");
        parametersContainer.RectMinSize = new Vector2(1920, 0.05F * 1080);
    }

    private void OfficeAction(int officeNumber)
    {
        try
        {
            _selectedOfficeNumber = officeNumber;
            var office = Core.Domain.GetOffice(officeNumber);
            if (Core.Domain.IsOfficeMine(officeNumber))
            {
                OpenOfficeAction(office);
                return;
            }

            if (office.OwnerId == -1)
            {
                RentOfficeAction(officeNumber);
                return;
            }

            ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = true;
            ShowAcceptDialog("Office " + officeNumber, "Unfortunately, this office is already occupied by the player "
                                                       + Core.Domain.GetPlayer(office.OwnerId).Name + ".", "ExitMenu");
        }
        catch (Exception e)
        {
            GD.Print(e);
        }
    }

    private void RentOfficeAction(int officeNumber)
    {
        ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = true;
        var office = Core.Domain.GetOffice(officeNumber);
        var message = "This office is vacant." + Environment.NewLine
                                               + "Rental price:" + office.RentalPrice + Environment.NewLine
                                               + "Capacity:" + office.Capacity + Environment.NewLine;
        if (Core.Domain.PlayerActor.Money < office.RentalPrice)
        {
            message += "You don't have enough money to rent!";
            ShowAcceptDialog("Office " + officeNumber, message, "ExitMenu");
            return;
        }

        message += "Rent this office?";
        ShowConfirmationDialog("Office " + officeNumber, message, "RentOffice", "ExitMenu");
    }

    private async Task RentOffice()
    {
        try
        {
            if (_selectedOfficeNumber == -1)
            {
                throw new Exception("Selected office is null!");
            }

            ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = true;
            var number = _selectedOfficeNumber;
            _selectedOfficeNumber = -1;
            if (!(await Core.Domain.RentOfficeAsync(number)))
            {
                ShowAcceptDialog("Error!", "Something went wrong when renting the office.", "ExitMenu");
                return;
            }

            ShowAcceptDialog("Office " + number, "You have successfully rented this office.", "ExitMenu");
        }
        catch (Exception e)
        {
            GD.Print(e);
        }
    }

    private void OpenOfficeAction(OfficeModel office)
    {
        ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = true;
        ShowOfficeMenu(office);
    }

    private void AuctionAction()
    {
        ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = true;
        ShowAcceptDialog("Auction house", "There are no auctions yet.", "ExitMenu");
    }

    private void ExitMenu()
    {
        while (_framesContainer.GetChildCount() > 1)
        {
            _framesContainer.RemoveChild(_framesContainer.GetChild(1));
        }

        ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = false;
    }

    private void ShowMenu(Node menu)
    {
        ExitMenu();
        _framesContainer.AddChild(menu);
        ((Viewport)_mapContainer.GetChild(0)).GuiDisableInput = true;

        if (!(menu is BasePopupMenu popupMenu))
        {
            return;
        }
        
        popupMenu.InitializeFields(Core);
        popupMenu.Exit = ExitMenu;
    }

    private void ShowWaitingMenu()
    {
        var menu = (WaitingMenu)((PackedScene)GD.Load("res://Scenes/GameFrames/WaitingMenu.tscn")).Instance();
        ShowMenu(menu);
        menu.Header.Text = Core.LanguageProvider.Wait + "!";
        menu.Message.Text = Core.LanguageProvider.ConnectingPlayers + "." + Environment.NewLine
                            + Core.LanguageProvider.TimeLeft + ":";
        Task.Run(() =>
        {
            while (Core.Domain.GameStage == GameStages.Connection)
            {
                menu.Time.Text = $"{Core.Domain.Time / 60:D2}:{Core.Domain.Time % 60:D2}";
                menu.ProgressBar.Value =
                    1 - Core.Domain.Time / 60.0; // 1 - (double)_client.Time / Settings.ConnectionRealTime;
            }

            ExitMenu();
            ShowChoosingBackgroundMenu();
        });
    }

    private void ShowChoosingBackgroundMenu()
    {
        var menu = (ChoosingBackgroundMenu)((PackedScene)GD.Load("res://Scenes/GameFrames/ChoosingBackgroundMenu.tscn"))
            .Instance();
        ShowMenu(menu);
        menu.Header.Text = Core.LanguageProvider.GameStages[2];
        menu.Buttons[0].Text = Core.LanguageProvider.Programmer;
        menu.Buttons[1].Text = Core.LanguageProvider.Designer;
        menu.Buttons[2].Text = Core.LanguageProvider.Musician;
        menu.Buttons[3].Text = Core.LanguageProvider.Manager;
        menu.Buttons[4].Text = Core.LanguageProvider.Major;
        menu.Domain = Core.Domain;
        menu.Exit = ExitMenu;
    }

    private void ShowOfficeMenu(OfficeModel office)
    {
        var menu = (OfficeMenu)((PackedScene)GD.Load("res://Scenes/GameFrames/OfficeMenu.tscn")).Instance();
        menu.Number = _selectedOfficeNumber;
        menu.Office = office;
        ShowMenu(menu);
    }

    private async Task SkipButtonPressed()
    {
        await Core.Domain.SkipMove(new ActionRequest()
        {
            PlayerId = 0
        });
    }
}