using System.Collections.ObjectModel;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Shared.Contracts.Game.Others;

namespace PmSim.Frontend.App.ViewModels.Frames;

public class OpportunityMenuViewModel : BasicFrameViewModel
{
    public ObservableCollection<OpportunityModel> Opportunities { get; } = new();
    
    public OpportunityMenuViewModel(GameScreenViewModel gameScreen) 
        : base(gameScreen)
    {
        var o = new OpportunityModel
        {
            Name = "Развод сотрудника",
            Description = "Брак вашего сотрудника внезапно распался. Вы любезно предложили пожить в офисе, попутно решив пару рабочих задач."
        };
        Opportunities.Add(o);
    }
}