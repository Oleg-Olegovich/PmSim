using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PmSim.Frontend.App.Views.Frames;

public partial class GameMapFrameView : UserControl
{
    public GameMapFrameView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}