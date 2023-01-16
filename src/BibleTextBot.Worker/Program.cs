using BibleTextBot.Worker;
using BibleTextBot.Worker.ApplicationCore.Interfaces;
using BibleTextBot.Worker.ApplicationCore.Services;
using BibleTextBot.Worker.Infrastructure.Data;
using Serilog;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IContext, Context>();
        services.AddTransient<IBotBibleTextService, BotBibleTextService>();
        services.AddHostedService<Worker>();
    })
    .Build();


await host.RunAsync();