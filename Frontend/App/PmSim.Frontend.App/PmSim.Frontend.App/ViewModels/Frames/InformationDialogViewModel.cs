﻿using PmSim.Frontend.App.ViewModels.Screens;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class InformationDialogViewModel : BasicFrame
{
    public string Info { get; }

    public InformationDialogViewModel(GameScreenViewModel gameScreen, string info)
        : base(gameScreen)
    {
        Info = info;
    }
}