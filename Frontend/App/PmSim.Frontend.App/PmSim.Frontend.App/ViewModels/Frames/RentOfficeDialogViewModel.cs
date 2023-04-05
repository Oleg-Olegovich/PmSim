using System.Reactive;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game.GameObjects.Others;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class RentOfficeDialogViewModel : BasicFrame
{
    private readonly int _number, _rentalPrice;

    public string Info { get; }
    
    public ReactiveCommand<Unit, Unit> RentCommand { get; }
    
    public ReactiveCommand<Unit, Unit> CancelCommand { get; }
    
    public RentOfficeDialogViewModel(GameScreenViewModel gameScreen, Office office, int number) 
        : base(gameScreen)
    {
        _number = number;
        _rentalPrice = office.RentalPrice;
        Info = string.Format(LocalizationGameScreen.OfficeRentInfo, office.RentalPrice, office.Capacity);
        RentCommand = ReactiveCommand.Create(Rent);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    private void Rent()
        => GameScreen.RentOffice(_number, _rentalPrice);

    private void Cancel()
        => GameScreen.ShowMapMenu();
}