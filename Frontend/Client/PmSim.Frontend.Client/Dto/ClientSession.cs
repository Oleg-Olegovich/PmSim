namespace PmSim.Frontend.Client.Dto
{
    public class ClientSession
    {
        public string AccessToken { get; set; }

        public ClientSession() { }

        public ClientSession(string token)
            => AccessToken = token;
    }
}