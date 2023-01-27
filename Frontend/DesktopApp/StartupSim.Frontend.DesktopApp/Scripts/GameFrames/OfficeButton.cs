using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class OfficeButton : GameMapButton
{
    public int Index { get; set; }

    protected override void OnMouseClick()
    {
        GameScreen.MakeAction(ActionTypes.Office, Index);
    }
}