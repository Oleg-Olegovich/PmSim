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
        var images = new[]
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
        OfficeImages = new ObservableCollection<IImage>
        {
            images[0],
            images[1],
            images[2],
            images[3],
            images[4],
            images[5],
            images[6],
            images[7],
            images[8],
            images[9],
            
            images[0],
            images[1],
            images[2],
            images[3],
            images[4],
            images[5],
            images[6],
            images[7],
            images[8],
            images[9],
            
            LoadImage($"{BasePath}small_office_0_normal_mine.png"),
            LoadImage($"{BasePath}small_office_1_normal_mine.png"),
            LoadImage($"{BasePath}small_office_2_normal_mine.png"),
            LoadImage($"{BasePath}small_office_0_normal_mine.png"),
            LoadImage($"{BasePath}small_office_0_normal_mine.png"),
            LoadImage($"{BasePath}middle_office_0_normal_mine.png"),
            LoadImage($"{BasePath}middle_office_1_normal_mine.png"),
            LoadImage($"{BasePath}middle_office_0_normal_mine.png"),
            LoadImage($"{BasePath}big_office_0_normal_mine.png"),
            LoadImage($"{BasePath}big_office_0_normal_mine.png"),
            
            LoadImage($"{BasePath}small_office_0_normal_not_mine.png"),
            LoadImage($"{BasePath}small_office_1_normal_not_mine.png"),
            LoadImage($"{BasePath}small_office_2_normal_not_mine.png"),
            LoadImage($"{BasePath}small_office_0_normal_not_mine.png"),
            LoadImage($"{BasePath}small_office_0_normal_not_mine.png"),
            LoadImage($"{BasePath}middle_office_0_normal_not_mine.png"),
            LoadImage($"{BasePath}middle_office_1_normal_not_mine.png"),
            LoadImage($"{BasePath}middle_office_0_normal_not_mine.png"),
            LoadImage($"{BasePath}big_office_0_normal_not_mine.png"),
            LoadImage($"{BasePath}big_office_0_normal_not_mine.png"),
        };
    }
}