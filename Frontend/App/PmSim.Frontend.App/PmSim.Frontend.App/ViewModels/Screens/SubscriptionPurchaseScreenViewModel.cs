using System.Reactive;
using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SubscriptionPurchaseScreenViewModel : BasicScreenViewModel
{
    private int _moneyAmount;
    
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
    
    public SubscriptionPurchaseScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel? previous) 
        : base(baseWindow, previous)
        => PurchaseCommand = ReactiveCommand.Create(ProcessSubscriptionPurchase);

    private void ProcessSubscriptionPurchase()
    {
        ProcessBackClick();
    }
}