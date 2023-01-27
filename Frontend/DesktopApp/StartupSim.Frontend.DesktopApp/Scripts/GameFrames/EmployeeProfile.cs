using Godot;
using System;

public class EmployeeProfile : VBoxContainer
{
    public Label NameLabel { get; set; }
    public Label BusynessLabel { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        NameLabel = (Label) FindNode("BusynessLabel");
        BusynessLabel = (Label) FindNode("BusynessLabel");
    }
}