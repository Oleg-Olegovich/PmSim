using PmSim.Frontend.Client.Dto;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using PmSim.Frontend.Client.Exceptions;
using PmSim.Frontend.Client.FileManagement;

namespace PmSim.Frontend.Client;

public static class ConfigurationManager
{
    public static ClientOptions ClientOptions
    {
        get
        {
            var host = InitializeHost();
            var clientOptions = host.Services.GetRequiredService<ClientOptions>();
            return clientOptions ?? throw new PmSimClientException("Client options is null!");
        }
    }
    
    private static IHost InitializeHost()
        => new HostBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("client_config.json", false, true);
            })
            .ConfigureServices((context, services) =>
            {
                var baseUriLink = context.Configuration.GetSection("BaseUri").Get<string>();
                if (baseUriLink is null)
                {
                    throw new PmSimClientException("Base uri is null!");
                }
                var baseUri = new Uri(baseUriLink);
                var clientOptions = new ClientOptions(baseUri);
                services.AddSingleton(clientOptions);
            })
            .Build();
}