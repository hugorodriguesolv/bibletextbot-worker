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
            .Enrich.FromLogContext()
            .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri)) { AutoRegisterTemplate = true })
            .CreateLogger();

        return base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _botTextoBiblicoService.GetBibleTextAsync();
            await Task.Delay(1000, stoppingToken);
        }

        _logger.LogInformation("Worker stoped at: {time}", DateTimeOffset.Now);
    }
}