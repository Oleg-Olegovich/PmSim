namespace PmSim.Backend.Gateway;

public class Startup
{
    private const int ApiVersion = 1;
    private const string ApiTitle = "User";

    private readonly IConfiguration _configuration;
    private const string AuthenticationOptionsSection = "Authentication";

    private const string UsersApiClientOptionsSection = "InternalApi:Users";
    private const string TimeseriesApiClientOptionsSection = "InternalApi:Timeseries";
    private const string AssetsApiClientOptionsSection = "InternalApi:Assets";

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration.GetSection("OneGate");
    }

    public void ConfigureServices(IServiceCollection services)
    {
        /*
        
        // Newtonsoft as serializer.
        services.AddControllers(p =>
            {
                p.Filters.Add<ExceptionFilter>();
            })
            .ConfigureBaseValidator()
            .AddNewtonsoftJson();

        // Versioning.
        services.AddBaseVersioning(ApiVersion);

        // Authentication.
        var authenticationSection = _configuration.GetSection(AuthenticationOptionsSection);

        services.AddBaseAuthentication(authenticationSection.Get<JwtOptions>());
        services.Configure<JwtOptions>(authenticationSection);

        // Enforce to use lowercase.
        services.AddRouting(options => options.LowercaseUrls = true);

        // Swagger.
        services.AddBaseSwagger(ApiTitle, ApiVersion);

        // Automapper.
        services.AddAutoMapper(p =>
            p.AddProfile<MappingProfile>()
        );
            
        // Users API.
        var usersApiClientSection = _configuration.GetSection(UsersApiClientOptionsSection);
        services.Configure<UsersApiClientOptions>(usersApiClientSection);
            
        services.AddTransient<IUsersApiClient, UsersApiClient>();
            
        // Timeseries API.
        var timeseriesApiClientSection = _configuration.GetSection(TimeseriesApiClientOptionsSection);
        services.Configure<TimeseriesApiClientOptions>(timeseriesApiClientSection);
            
        services.AddTransient<ITimeseriesApiClient, TimeseriesApiClient>();

        // Assets API.
        var assetsApiClientSection = _configuration.GetSection(AssetsApiClientOptionsSection);
        services.Configure<AssetsApiClientOptions>(assetsApiClientSection);
            
        services.AddTransient<IAssetsApiClient, AssetsApiClient>();
        
        */
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Routing.
        app.UseRouting();

        // Authorization.
        app.UseAuthentication();
        app.UseAuthorization();

        // Prometheus metrics.
        //app.UseHttpMetrics();

        // Endpoints.
        app.UseEndpoints(endpoints =>
        {
            // Api.
            endpoints.MapControllers();
            // Prometheus.
            //endpoints.MapMetrics("/metrics");
        });

        // Swagger.
        //app.UseBaseSwagger(ApiVersion);
    }
}