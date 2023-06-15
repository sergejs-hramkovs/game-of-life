using GameOfLife;
using GameOfLife.Interfaces;
using GameOfLife.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IMainEngine, MainEngine>();
        services.AddScoped<IAuxiliaryEngine, AuxiliaryEngine>();
    })
    .Build();

Launcher launcher = new Launcher();
launcher.LaunchGame();