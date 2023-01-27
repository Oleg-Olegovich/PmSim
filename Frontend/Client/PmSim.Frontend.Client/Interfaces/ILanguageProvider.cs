namespace PmSim.Frontend.Domain.Interfaces
{

    /// <summary>
    /// This interface is necessary for the elegant implementation of 
    /// support for localizations of the interface in different languages.
    /// All classes containing language localizations are inherited from it.
    /// </summary>
    public interface ILanguageProvider
    {
        string LanguageName { get; }
        string[] MaleNames { get; }
        string[] MaleSurnames { get; }
        string[] FemaleNames { get; }
        string[] FemaleSurnames { get; }
        string[] ProjectNames { get; }
        string[] FeaturesNames { get; }
        string[] OpportunityNames { get; }
        string[] OpportunityDescriptions { get; }
        string[] IncidentNames { get; }
        string[] IncidentDescriptions { get; }
        string Singleplayer { get; }
        string Multiplayer { get; }
        string Options { get; }
        string Exit { get; }
        string Back { get; }
        string GameSettings { get; }
        string ConnectionRealTime { get; }
        string ChoosingBackgroundRealTime { get; }
        string SprintRealTime { get; }
        string DiplomacyRealTime { get; }
        string IncidentRealTime { get; }
        string SprintActionsNumbers { get; }
        string AuctionRealTime { get; }
        string StartUpCapital { get; }
        string Start { get; }
        string SignIn { get; }
        string SignUp { get; }
        string Password { get; }
        string RepeatPassword { get; }
        string Login { get; }
        string NewGame { get; }
        string Connect { get; }
        string GamesList { get; }
        string ConfirmEmail { get; }
        string ConfirmationCode { get; }
        string Confirm { get; }
        string GiveUp { get; }
        string Players { get; }
        string Name { get; }
        string Next { get; }
        string InputName { get; }
        string Skip { get; }
        string ConnectingPlayers { get; }
        string TimeLeft { get; }
        string Wait { get; }
        string ConductInterview { get; }
        string CancelLease { get; }
        string DismissEveryone { get; }
        string Cancel { get; }
        string HireTechSupportOfficer { get; }
        string[] GameStages { get; }
        string Default { get; }
        string Programmer { get; }
        string Designer { get; }
        string Musician { get; }
        string Manager { get; }
        string Major { get; }
    }
}