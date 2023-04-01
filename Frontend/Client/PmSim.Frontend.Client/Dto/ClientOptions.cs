using System;

namespace PmSim.Frontend.Client.Dto
{
    public class ClientOptions
    {
        /// <summary>
        /// The main part of the requests link.
        /// </summary>
        public Uri? BaseUri { get; } = new("https://t.me/treeverse_games/");
        
        /// <summary>
        /// JWT is obtained during authorization.
        /// </summary>
        public string? AccessToken { get; set; }
        
        /// <summary>
        /// Id is obtained during authorization.
        /// </summary>
        public int ClientId { get; set; }
        
        /// <summary>
        /// Id is obtained during connection to a game.
        /// </summary>
        public int GameId { get; set; }

        public ClientOptions()
        { }
    }
}