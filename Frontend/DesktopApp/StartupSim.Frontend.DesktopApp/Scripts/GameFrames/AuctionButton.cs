using StartupSim.Frontend.DesktopApp.Scripts.Enums;

public class AuctionButton : GameMapButton
{
    protected override void OnMouseClick()
    {
        GameScreen.MakeAction(ActionTypes.Auction);
    }
}