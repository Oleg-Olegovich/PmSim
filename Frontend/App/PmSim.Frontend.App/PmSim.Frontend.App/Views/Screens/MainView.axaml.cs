using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PmSim.Frontend.App.Views.Screens;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}