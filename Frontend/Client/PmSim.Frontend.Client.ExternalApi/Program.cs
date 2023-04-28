using PmSim.Frontend.Client.Api;

namespace PmSim.Frontend.Client.ExternalApi;

internal static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        AddEndpoints(app);
        app.Run();
    }

    private static void AddEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/game_status/actions_number",
            (int number) =>
            {
                if (MultiPlayerClient.Client is null)
                {
                    return;
                }

                MultiPlayerClient.Client.ActionsNumber = number;
            });
        
        app.MapPost("/api/game_status/money",
            (int money) =>
            {
                if (MultiPlayerClient.Client is null)
                {
                    return;
                }

                MultiPlayerClient.Client.Money = money;
            });
    }
}