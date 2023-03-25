﻿using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.Client;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SingleSignInScreenViewModel : BasicScreenViewModel
{
    private string _login = "";
    
    public string Login
    {
        get => _login;
        set
        {
            this.RaiseAndSetIfChanged(ref _login, value);
            if (IsDataRemembered)
            {
                BaseWindow.Options.AutofillUserData.SingleLogin = Login;
            }
        }
    }

    private bool _isDataRemembered = true;
    
    public bool IsDataRemembered
    {
        get => _isDataRemembered;
        set
        {
            this.RaiseAndSetIfChanged(ref _isDataRemembered, value);
            BaseWindow.Options.AutofillUserData.IsSingleDataRemembered = value;
            if (value)
            {
                BaseWindow.Options.AutofillUserData.SingleLogin = Login;
            }
        }
    }

    public ReactiveCommand<Unit, Unit> NextCommand { get; }

    public SingleSignInScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel previous)
        : base(baseWindow, previous)
    {
        NextCommand = ReactiveCommand.Create(OpenGameOptionsScreen);
        if (!baseWindow.Options.AutofillUserData.IsSingleDataRemembered)
        {
            Login = baseWindow.Options.AutofillUserData.SingleLogin ?? "";
        }
    }

    private void OpenGameOptionsScreen()
    {
        var client = new PmSimClient(Login);
        BaseWindow.Content = new GameOptionsScreenViewModel(BaseWindow, this, client);
    }
}