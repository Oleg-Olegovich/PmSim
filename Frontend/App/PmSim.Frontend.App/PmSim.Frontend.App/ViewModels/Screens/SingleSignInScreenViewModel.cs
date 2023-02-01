using PmSim.Frontend.App.ViewModels.Windows;
using ReactiveUI;

namespace PmSim.Frontend.App.ViewModels.Screens;

public class SingleSignInScreenViewModel : BasicScreenViewModel
{
    public SingleSignInScreenViewModel(BasicWindowViewModel baseWindow, BasicScreenViewModel? previousScreen = null) 
        : base(baseWindow, previousScreen)
    {
        private string _name = "";

        /// <summary>
        /// Fullscreen mode flag, switches to view.
        /// </summary>
        public string Name
        {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }
        
        private bool _isNameRemembered;

        /// <summary>
        /// Name remember mode flag.
        /// </summary>
        public bool IsNameRemembered
        {
            get => _isNameRemembered;
            set => this.RaiseAndSetIfChanged(ref _isNameRemembered, value);
        }
    }
}