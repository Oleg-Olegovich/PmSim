﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PmSim.Frontend.App.Views.Frames;

public partial class OpportunityMenuView : UserControl
{
    public OpportunityMenuView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}