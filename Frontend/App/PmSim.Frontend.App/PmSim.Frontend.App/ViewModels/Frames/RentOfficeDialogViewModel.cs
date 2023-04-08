using System.Reactive;
using PmSim.Frontend.App.Properties.Localizations;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game.Others;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class RentOfficeDialogViewModel : BasicFrameViewModel
{
    private readonly int _id, _rentalPrice;

    public string Info { get; }
    
    public ReactiveCommand<Unit, Unit> RentCommand { get; }

    public RentOfficeDialogViewModel(GameScreenViewModel gameScreen, Office office, int id) 
        : base(gameScreen)
    {
        _id = id;
        _rentalPrice = office.RentalPrice;
        Info = string.Format(LocalizationGameScreen.OfficeRentInfo, office.RentalPrice, office.Capacity);
        RentCommand = ReactiveCommand.Create(Rent);
    }

    private void Rent()
        => GameScreen.RentOffice(_id, _rentalPrice);
}