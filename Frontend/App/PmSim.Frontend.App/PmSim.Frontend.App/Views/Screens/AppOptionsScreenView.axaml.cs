using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PmSim.Frontend.App.Views.Screens;

public partial class AppOptionsScreenView : UserControl
{
    public AppOptionsScreenView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}