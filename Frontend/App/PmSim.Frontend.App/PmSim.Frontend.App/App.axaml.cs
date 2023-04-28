using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PmSim.Frontend.App.ViewModels.Screens;
using PmSim.Frontend.App.ViewModels.ThemesManagement;
using PmSim.Frontend.App.ViewModels.Windows;
using PmSim.Frontend.App.Views.Screens;
using PmSim.Frontend.App.Views.Windows;
using PmSim.Frontend.Client.FileManagement;
using Serilog;

namespace PmSim.Frontend.App;

public class App : Application
{
    public override void Initialize() 
        => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        switch (ApplicationLifetime)
        {
            case IClassicDesktopStyleApplicationLifetime desktop:
                var host = InitializeHost();
                if (host is null)
                {
                    throw new NullReferenceException("Host is null!");
                }
                
                desktop.MainWindow = new MainWindowView
                {
                    DataContext = host.Services.GetRequiredService<MainWindowViewModel>()
                };
                break;
            case ISingleViewApplicationLifetime singleViewPlatform:
                if (Current != null)
                {
                    ThemesManager.Theme = Current.ActualThemeVariant == ThemeVariant.Dark 
                        ? Themes.DarkSimple : Themes.LightSimple;
                }
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel()
                };
                break;
        }
        
        base.OnFrameworkInitializationCompleted();
    }
    
    private static IHost InitializeHost()
        => new HostBuilder()
            .ConfigureAppConfiguration(config 
                => config.AddJsonFile(FileManager.GetConfigurationFileName(typeof(AppOptions)), 
                    true, true))
            .ConfigureServices((context, services) =>
            {
                var appOptions = context.Configuration.GetSection(nameof(AppOptions)).Get<AppOptions>() 
                                 ?? AppOptions.Default;
                services.AddSingleton(appOptions);
                var log = new LoggerConfiguration()
                    .WriteTo.File(Constants.LogFileFullName, rollingInterval: RollingInterval.Day)
                    .CreateLogger();
                services.AddSingleton(log);
                services.AddSingleton<MainWindowViewModel>();
            })
            .Build();
}