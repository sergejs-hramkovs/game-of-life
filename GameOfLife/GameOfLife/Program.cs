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
        services.AddScoped<IInputController, InputController>();
        services.AddScoped<ILibrary, Library>();
        services.AddScoped<IMenuNavigator, MenuNavigator>();
        services.AddScoped<IRenderer, Renderer>();
        services.AddScoped<IRulesApplier, RulesApplier>();
        services.AddScoped<IUserInterfaceFiller, UserInterfaceFiller>();
        services.AddScoped<IMainEngine, MainEngine>();
    })
    .Build();

var service = host.Services.GetRequiredService<IMainEngine>();
service.StartGame();