using GameOfLife;
using GameOfLife.Interfaces;
using GameOfLife.Services;
using GameOfLife.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddScoped<IAuxiliaryEngine, AuxiliaryEngine>();
        services.AddScoped<IFieldOperations, FieldOperations>();
        services.AddScoped<IFileIO, FileIO>();
        services.AddScoped<IInputProcessorService, InputProcessorService>();
        services.AddScoped<ILibrary, Library>();
        services.AddScoped<IMenuNavigator, MenuNavigator>();
        services.AddScoped<IConsoleApplicationRenderingService, ConsoleApplicationRenderingService>();
        services.AddScoped<IGameFieldService, GameFieldService>();
        services.AddScoped<IUserInterfaceFiller, UserInterfaceFiller>();
        services.AddScoped<IMainEngine, MainEngine>();
    })
    .Build();

var service = host.Services.GetRequiredService<IMainEngine>();
service.StartGame();