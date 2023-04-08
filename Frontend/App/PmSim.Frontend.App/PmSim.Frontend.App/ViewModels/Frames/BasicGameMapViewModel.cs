using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Enums;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public abstract class BasicGameMapViewModel : BasicFrameViewModel
{
    protected readonly string BasePath;
    
    public abstract ObservableCollection<IImage> OfficeImages { get; }

    public ReactiveCommand<string, Unit> OfficeCommand { get; }
    
    public ReactiveCommand<Unit, Unit> AuctionCommand { get; }
    
    protected BasicGameMapViewModel(GameScreenViewModel gameScreen, int mapNumber)
        : base(gameScreen)
    {
        OfficeCommand = ReactiveCommand.Create<string>(ProcessOfficeClick);
        AuctionCommand = ReactiveCommand.Create(ProcessAuctionClick);
        BasePath = $"avares://PmSim.Frontend.App/Assets/Images/Map{mapNumber}/";
    }

    public void ChangeOfficeImage(int officeId, OfficeStates state)
    {
        var officeType = officeId switch
        {
            < 5 => "small_office_0_normal",
            < 8 => "middle_office_0_normal",
            _ => "big_office_0_normal"
        };
        var basePath = $"{BasePath}Map0/{officeType}";
        var id = state switch
        {
            OfficeStates.Unoccupied => 10,
            OfficeStates.Mine => 20,
            _ => 30
        };

        OfficeImages[officeId] = OfficeImages[officeId + id];
    }
    
    protected static IImage LoadImage(string path)
    {
        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
        var stream = assets?.Open(new Uri(path));
        return new Bitmap(stream!);
    }
    
    private void ProcessOfficeClick(string officeId) 
        => GameScreen.ShowOffice(int.Parse(officeId));
    
    private void ProcessAuctionClick()
        => GameScreen.ShowAuctionHouseMenu();
}