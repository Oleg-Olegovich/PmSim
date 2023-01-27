using Godot;

public class WaitingMenu : Node
{
    public Label Header { get; private set; }

    public Label Message { get; private set; }
    
    public Label Time { get; private set; }

    public ProgressBar ProgressBar { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Header = (Label) FindNode("HeaderLabel");
        Message = (Label) FindNode("MessageLabel");
        Time = (Label) FindNode("TimeLabel");
        ProgressBar = (ProgressBar) FindNode("ProgressBar");
    }
}