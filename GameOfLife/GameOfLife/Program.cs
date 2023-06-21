using GameOfLife;
using GameOfLife.Interfaces;
using GameOfLife.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Engine;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IFieldSeedingService, FieldSeedingService>();
        services.AddScoped<IFileIO, FileIO>();
        services.AddScoped<IInputProcessorService, InputProcessorService>();
        services.AddScoped<ILibrary, Library>();
        services.AddScoped<IGameFieldService, GameFieldService>();
        services.AddScoped<IUserInterfaceService, UserInterfaceService>();
        services.AddScoped<IMainEngine, MainEngine>();
        services.AddScoped<IRenderingService, RenderingService>();
    })
    .Build();

var service = host.Services.GetRequiredService<IMainEngine>();
service.StartGame();