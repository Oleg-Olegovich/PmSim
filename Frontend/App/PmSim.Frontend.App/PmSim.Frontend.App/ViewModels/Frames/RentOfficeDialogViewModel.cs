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
    
    public bool IsFree { get; }
    
    public ReactiveCommand<Unit, Unit> RentCommand { get; }

    public RentOfficeDialogViewModel(GameScreenViewModel gameScreen, Office office, int id) 
        : base(gameScreen)
    {
        _id = id;
        _rentalPrice = office.RentalPrice;
        RentCommand = ReactiveCommand.Create(Rent);
        IsFree = office.OwnerId == -1;
        Info = string.Format(IsFree 
            ? LocalizationGameScreen.OfficeRentInfo 
            : LocalizationGameScreen.OfficeCancelRentInfo, office.RentalPrice, office.Capacity);
    }

    private void Rent()
    {
        if (IsFree)
        {
            GameScreen.RentOffice(_id, _rentalPrice);
            return;
        }
        
        GameScreen.CancelOfficeLease(_id);
    }
}