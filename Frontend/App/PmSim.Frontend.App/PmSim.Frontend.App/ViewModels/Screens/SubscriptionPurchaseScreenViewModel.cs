using System.Reactive;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SubscriptionPurchaseScreenViewModel : BasicScreenViewModel
{
    private readonly GamesListScreenViewModel _gamesListScreen;
    
    private int _moneyAmount = 10;
    
    public int MoneyAmount
    {
        get => _moneyAmount;
        set
        {
            this.RaiseAndSetIfChanged(ref _moneyAmount, value);
            IsPurchaseAvailable = value != 0;
        }
    }

    private bool _isPurchaseAvailable;
    
    public bool IsPurchaseAvailable
    {
        get => _isPurchaseAvailable;
        set => this.RaiseAndSetIfChanged(ref _isPurchaseAvailable, value);
    }
    
    private string _errorMessage = "";
    
    public string ErrorMessage
    {
        get => _errorMessage;
        set => this.RaiseAndSetIfChanged(ref _errorMessage, value);
    }
    
    public ReactiveCommand<Unit, Unit> PurchaseCommand { get; }
    
    public SubscriptionPurchaseScreenViewModel(MainViewModel mainView, BasicScreenViewModel? previous,
        GamesListScreenViewModel gamesListScreen, bool showError = false) 
        : base(mainView, previous)
    {
        PurchaseCommand = ReactiveCommand.Create(ProcessSubscriptionPurchase);
        _gamesListScreen = gamesListScreen;
        if (showError)
        {
            ErrorMessage = LocalizationSubscriptionPurchaseScreen.NeedToPay;
        }
    }

    private void ProcessSubscriptionPurchase()
    {
        MainView.Content = _gamesListScreen;
    }
}