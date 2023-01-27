using PmSim.Frontend.Domain.Interfaces;

namespace PmSim.Frontend.Domain.Languages.English
{

    public class EnglishLanguageProvider : ILanguageProvider
    {
        public string LanguageName
            => "English";

        public string[] MaleNames => new[]
        {
            "Yuri",
            "Alexey",
            "Peter",
            "Quentin",
            "Samuel"
        };

        public string[] MaleSurnames => new[]
        {
            "Sidorov",
            "Jackson",
            "Lazy",
            "Workaholic",
            "Clark"
        };

        public string[] FemaleNames => new[]
        {
            "Alexandra",
            "Julia",
            "Lara",
            "Isabella",
            "Catherine"
        };

        public string[] FemaleSurnames => new[]
        {
            "Sidorova",
            "Jackson",
            "Lazy",
            "Workaholic",
            "Clark"
        };

        public string[] ProjectNames => new[]
        {
            "Social Network",
            "Messenger",
            "Online store",
            "Video hosting",
            "Image hosting",
            "Forum",
            "News Portal",
            "Voting Service",
            "2d arcade",
            "Platformer",
            "Racing game",
            "Shooter",
            "Startup Simulator",
            "Email",
            "Graphic Editor",
            "Online Graphic Editor",
            "Smart calculator",
            "School magazine",
            "AI",
        };

        public string[] FeaturesNames => new[]
        {
            "Built-in audio player",
            "Audio messages",
            "Mini-chat",
        };

        public string[] OpportunityNames => new[]
        {
            "Employee's divorce",
        };

        public string[] OpportunityDescriptions => new[]
        {
            "Your employee's marriage suddenly broke up. You kindly offered to live in the office, "
            + "simultaneously solving a couple of work tasks.",
        };

        public string[] IncidentNames => new[]
        {
            "Internet outage",
            "Power failure",
            "Hacker attack"
        };

        public string[] IncidentDescriptions => new[]
        {
            "Due to the frequent interruptions of the Internet, the main provider of the city plans to turn off the network for prevention."
            + " In this case, no one will receive income on this sprint. However, the provider promises to find another solution for "
            + "a little help ...",
            "An emergency situation has been declared at the power plant. In case of a power failure in the middle of the working day, "
            + " everyone will lose unsaved data. To avoid this, it is necessary to raise money for urgent repairs",
            "Hackers threaten to delete one repository of each IT startup and promise not to do it if they transfer some money."
        };
        
        public string Singleplayer => "Singleplayer";

        public string Multiplayer => "Multiplayer";

        public string Options => "Options";

        public string Exit => "Exit";
        
        public string Back => "Back";
        
        public string GameSettings => "Game settings";

        public string ConnectionRealTime => "Connection real time (sec.)";
        
        public string ChoosingBackgroundRealTime => "Choosing background real time (sec.)";
        
        public string SprintRealTime => "Sprint real time (sec.)";
        
        public string DiplomacyRealTime => "Diplomacy real time (sec.)";
        
        public string IncidentRealTime => "Incident real time (sec.)";
        
        public string SprintActionsNumbers => "Actions number per sprint";
        
        public string AuctionRealTime => "Auction real time (sec.)";
        
        public string StartUpCapital => "Start up capital";
        
        public string Start => "Start!";
        
        public string SignIn => "Sign in";
        
        public string SignUp => "Sign up";
        
        public string Password => "Password";
        
        public string RepeatPassword => "Repeat password";
        
        public string Login => "Login";
        
        public string NewGame => "New game";
        
        public string Connect => "Connect";
        
        public string GamesList => "Games list";
        
        public string ConfirmEmail => "Confirm the Email";
        
        public string ConfirmationCode => "Confirmation code";
        
        public string Confirm => "Confirm";
        
        public string GiveUp => "Give up";
        
        public string Players => "Players";
        
        public string Name => "Name";
        
        public string Next => "Next";
        
        public string InputName => "Input the name";
        
        public string Skip => "Skip";

        public string ConnectingPlayers => "Connecting players";

        public string TimeLeft => "Time left";

        public string Wait => "Wait";
        
        public string ConductInterview => "Conduct an interview";
        
        public string CancelLease => "Cancel the lease";
        
        public string DismissEveryone => "Dismiss everyone";
        
        public string Cancel => "Cancel";
        
        public string HireTechSupportOfficer => "Hire tech support officer";
        
        public string[] GameStages => new []{
            "Hasn't started yet",
            "Connection",
            "Choosing a background",
            "Sprint",
            "Diplomacy",
            "Summing up the results",
            "Discussion of the incident",
            "The incident resolution",
            "Is over",
        };
        
        public string Default => "Default";
        
        public string Programmer => "Programmer";
        public string Designer => "Designer";
        public string Musician => "Musician";
        public string Manager => "Manager";
        public string Major => "Silver spoon";
    }
}