using System.Collections.ObjectModel;
using Avalonia.Media;
using PmSim.Frontend.App.ViewModels.Screens;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class GameMap0ViewModel : BasicGameMapViewModel
{
    public override ObservableCollection<IImage> OfficeImages { get; }
    
    public GameMap0ViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen, 0)
    {
        OfficeImages = new ObservableCollection<IImage>
        {
            LoadImage($"{BasePath}small_office_0_normal.png"),
            LoadImage($"{BasePath}small_office_1_normal.png"),
            LoadImage($"{BasePath}small_office_2_normal.png"),
            LoadImage($"{BasePath}small_office_0_normal.png"),
            LoadImage($"{BasePath}small_office_0_normal.png"),
            LoadImage($"{BasePath}middle_office_0_normal.png"),
            LoadImage($"{BasePath}middle_office_1_normal.png"),
            LoadImage($"{BasePath}middle_office_0_normal.png"),
            LoadImage($"{BasePath}big_office_0_normal.png"),
            LoadImage($"{BasePath}big_office_0_normal.png")
        };
    }
}