using PmSim.Frontend.Client.LanguagesManager;
using PmSim.Frontend.Client.Properties;
using PmSim.Shared.Contracts.Enums;
using PmSim.Shared.Contracts.Game;

namespace PmSim.Frontend.Client.Api;

public interface IPmSimClient
{
    
    
    public string[] GetModes(Languages language)
    {
        LocalizationsProvider.Localization = language;
        return new[]
        {
            LocalizationGameModes.ResourceManager.GetString(GameModes.Survival.ToString())!,
            LocalizationGameModes.ResourceManager.GetString(GameModes.TimerAndMoney.ToString())!,
            LocalizationGameModes.ResourceManager.GetString(GameModes.TimerAndProjects.ToString())!
        };
    }

    public string[] GetMaps(Languages language)
    {
        LocalizationsProvider.Localization = language;
        return new[]
        {
            LocalizationGameMaps.Map0
        };
    }

    public Task CreateNewGameAsync(GameOptions gameOptions);
}