using Godot;

public class GameMapContainer : ViewportContainer
{
    public void AddMap(GameMap gameMap)
    {
        var viewport = (Viewport) FindNode("GameMapViewport");
        //_viewport = GetViewport();
        viewport.AddChild(gameMap);
    }
}