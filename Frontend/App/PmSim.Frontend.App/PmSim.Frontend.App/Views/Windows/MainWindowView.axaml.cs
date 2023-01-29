using Avalonia.Markup.Xaml;

namespace PmSim.Frontend.App.Views.Windows;

public partial class MainWindowView : BasicWindowView
{
    protected override void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}