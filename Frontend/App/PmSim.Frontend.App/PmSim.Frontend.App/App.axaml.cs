using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PmSim.Frontend.App.Models;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.App.Views.Windows;
using PmSim.Frontend.Client.FileManagement;
using Serilog;
using Size = PmSim.Frontend.App.ViewModels.Windows.Size;

namespace PmSim.Frontend.App;

public class App : Application
{
    private IHost? _host;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        InitializeHost();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (_host is null)
        {
            throw new NullReferenceException("Host is null!");
        }
        
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                desktop.MainWindow = new MainWindowView
                {
                    DataContext = _host.Services.GetRequiredService<MainWindowViewModel>()
                };
                break;
            case ISingleViewApplicationLifetime singleViewPlatform:
                singleViewPlatform.MainView = new MainWindowView
                {
                    DataContext = _host.Services.GetRequiredService<MainWindowViewModel>()
                };
                break;
        }

        base.OnFrameworkInitializationCompleted();
    }
    
    private void InitializeHost()
        => _host = new HostBuilder()
            .ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", false, true);
                config.AddJsonFile(FileManager.GetConfigurationFileName(typeof(AppOptions)), true, true);
            })
            .ConfigureServices((context, services) =>
            {
                AppOptions.Default = context.Configuration
                    .GetSection("Defaults")
                    .GetSection(nameof(AppOptions))
                    .Get<AppOptions>();
                Size.Default = context.Configuration
                    .GetSection("Defaults")
                    .GetSection(nameof(Size))
                    .Get<Size>();
                var appOptions = context.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>() 
                                 ?? AppOptions.Default 
                                 ?? throw new NullReferenceException("Default app settings is null!");
                services.AddSingleton(appOptions);
                services.AddSingleton<MainWindowViewModel>();
                var log = new LoggerConfiguration()
                    .WriteTo.File(Constants.LogFileFullName, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                services.AddSingleton(log);
            })
            .Build();
}