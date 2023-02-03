using Avalonia;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class BasicGameMapViewModel : ViewModelBase
{
    public Rect CanvasBounds
    {
        set
        {
            //MapImageWidth = value.Width;
            //MapImageHeight = value.Height;
        }
    }

    private double _mapImageWidth;
    
    public double MapImageWidth
    {
        get => _mapImageWidth;
        set => this.RaiseAndSetIfChanged(ref _mapImageWidth, value);
    }
    
    private double _mapImageHeight;
    
    public double MapImageHeight
    {
        get => _mapImageHeight;
        set => this.RaiseAndSetIfChanged(ref _mapImageHeight, value);
    }
}