using Godot;
using Godot.Collections;

public abstract class GameMapButton : TextureButton
{
    public GameScreen GameScreen { get; set; }

    public override void _Ready()
    {
        Connect("pressed", this, "OnMouseClick");
    }

    protected abstract void OnMouseClick();
}