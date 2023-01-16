namespace BibleTextBot.Worker;

using ApplicationCore.Interfaces;
using Serilog;
using Serilog.Sinks.Elasticsearch;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBotBibleTextService _botTextoBiblicoService;
    private readonly IConfiguration configuration;

    public Worker(ILogger<Worker> logger,
        IBotBibleTextService botTextoBiblicoService,
        IConfiguration configuration)
    {
        _logger = logger;
        _botTextoBiblicoService = botTextoBiblicoService;
        this.configuration = configuration;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var elasticUri = configuration
            .GetSection("ElasticSettings")
            .GetValue<string>("Uri");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .WriteTo.Console()
            .WriteTo.Debug(Serilog.Events.LogEventLevel.Information)
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions()
            {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name!.ToLower().Replace(".", "-")}-{Environment.MachineName?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
            })
            .CreateLogger();

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started executed at: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await _botTextoBiblicoService.GetBibleTextAsync();

            _logger.LogInformation("Worker next executed at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }
    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Worker stoped executed at: {time}", DateTimeOffset.Now);

        return base.StopAsync(cancellationToken);
    }
}