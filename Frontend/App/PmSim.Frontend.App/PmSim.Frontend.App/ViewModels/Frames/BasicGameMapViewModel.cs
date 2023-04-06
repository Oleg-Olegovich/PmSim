using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Avalonia;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Exceptions;
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
        switch (state)
        {
            case OfficeStates.Unoccupied:
                OfficeImages[officeId] = LoadImage($"{basePath}.png");
                break;
            case OfficeStates.Mine:
                OfficeImages[officeId] = LoadImage($"{basePath}_mine.png");
                break;
            case OfficeStates.NotMine:
            default:
                OfficeImages[officeId] = LoadImage($"{basePath}_not_mine.png");
                break;
        }
    }
    
    protected static IImage LoadImage(string path)
    {
        var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
        var stream = assets?.Open(new Uri(path));
        return new Bitmap(stream ?? throw new PmSimException("No office image found."));
    }
    
    private void ProcessOfficeClick(string officeId) 
        => GameScreen.ShowOffice(int.Parse(officeId));
    
    private void ProcessAuctionClick()
        => GameScreen.ShowAuctionHouseMenu();
}