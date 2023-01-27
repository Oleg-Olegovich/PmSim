using System;

namespace StartupSim.Frontend.Domain
{
    public class ClientOptions
    {
        public Uri BaseUri { get; }

        public string ClientKey { get; }

        public ClientOptions() { }

        public ClientOptions(Uri uri, string key)
        {
            BaseUri = uri;
            ClientKey = key;
        }
    }
}