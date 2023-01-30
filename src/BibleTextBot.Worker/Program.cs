using BibleTextBot.Worker;
using BibleTextBot.Worker.ApplicationCore.Interfaces;
using BibleTextBot.Worker.ApplicationCore.Services;
using BibleTextBot.Worker.Infrastructure.Data;
using Serilog;
using Serilog.Sinks.Elasticsearch;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddSingleton<IContext, Context>();
        services.AddTransient<IBotBibleTextService, BotBibleTextService>();
        services.AddHostedService<Worker>();

        Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
           .Enrich.FromLogContext()
           .Enrich.WithMachineName()
           .WriteTo.Console()
           .WriteTo.Debug(Serilog.Events.LogEventLevel.Information)
           .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
           {
               AutoRegisterTemplate = true,
               IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{Environment.MachineName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
           })
           .CreateLogger();
    })
    .Build();

await host.RunAsync();