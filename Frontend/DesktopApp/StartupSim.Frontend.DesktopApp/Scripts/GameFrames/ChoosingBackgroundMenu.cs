using System;
using System.Threading.Tasks;
using Godot;
using Godot.Collections;
using StartupSim.Backend.GameEngine.Enums;
using StartupSim.Backend.Gateway.Contracts.Actions;
using StartupSim.Frontend.Domain.Interfaces;

public class ChoosingBackgroundMenu : Node
{
    public Label Header { get; private set; }

    public Array<Button> Buttons { get; private set; }

    public Action Exit { get; set; }

    public IStartupSimDomain Domain { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Header = (Label) FindNode("HeaderLabel");
        Buttons = new Array<Button>(FindNode("ButtonsContainer").GetChildren());
        Buttons[0].Connect("pressed", this, "ProgrammerButton");
        Buttons[1].Connect("pressed", this, "DesignerButton");
        Buttons[2].Connect("pressed", this, "MusicianButton");
        Buttons[3].Connect("pressed", this, "ManagerButton");
        Buttons[4].Connect("pressed", this, "MajorButton");
    }

    private async Task ProgrammerButton()
    {
        await Domain.SetBackgroundAsync(new SetBackgroundRequest()
        {
            Profession = Professions.Programmer
        });
        Exit();
    }
    
    private async Task DesignerButton()
    {
        await Domain.SetBackgroundAsync(new SetBackgroundRequest()
        {
            Profession = Professions.Designer
        });
        Exit();
    }
    
    private async Task MusicianButton()
    {
        await Domain.SetBackgroundAsync(new SetBackgroundRequest()
        {
            Profession = Professions.Musician
        });
        Exit();
    }
    
    private async Task ManagerButton()
    {
        await Domain.SetBackgroundAsync(new SetBackgroundRequest()
        {
            Profession = Professions.Manager
        });
        Exit();
    }
    
    private async Task MajorButton()
    {
        await Domain.SetBackgroundAsync(new SetBackgroundRequest()
        {
            Profession = Professions.Major
        });
        Exit();
    }
}