using Godot;
using Godot.Collections;
using StartupSim.Backend.Gateway.Contracts.Game.GameObjects.Others;

public class OfficeMenu : BasePopupMenu
{
    private Label _infoLabel;
    
    public int Number { get; set; }

    public OfficeModel Office { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Label = (Label)FindNode("HeaderLabel");
        //_infoLabel = (Label)FindNode("OfficeInfoLabel");
        var n = FindNode("VBoxContainer");
        _infoLabel = (Label)FindNode("OfficeInfoLabel");
        Buttons = new Array<Button>(FindNode("ButtonsContainer").GetChildren());
        Buttons[0].Connect("pressed", this, "InterviewButtonPressed");
        Buttons[1].Connect("pressed", this, "CancelLeaseButtonPressed");
        Buttons[2].Connect("pressed", this, "DismissAllButtonPressed");
        Buttons[3].Connect("pressed", this, "TechSupportButtonPressed");
        Buttons[4].Connect("pressed", this, "OnExit");
        base._Ready();
        PopupCentered();
    }

    public override void InitializeFields(Main core)
    {
        Label.Text = "Office " + Number;
        _infoLabel.Text = "Capacity: " + Office.Capacity + "; " + "rental price: " + Office.RentalPrice;
        Buttons[0].Text = core.LanguageProvider.ConductInterview;
        Buttons[1].Text = core.LanguageProvider.CancelLease;
        Buttons[2].Text = core.LanguageProvider.DismissEveryone;
        Buttons[3].Text = core.LanguageProvider.HireTechSupportOfficer;
        Buttons[4].Text = core.LanguageProvider.Cancel;
    }

    private void InterviewButtonPressed()
    {
    }

    private void CancelLeaseButtonPressed()
    {
    }

    private void DismissAllButtonPressed()
    {
    }

    private void TechSupportButtonPressed()
    {
    }
}