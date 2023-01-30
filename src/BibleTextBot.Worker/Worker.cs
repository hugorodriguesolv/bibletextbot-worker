namespace BibleTextBot.Worker;

using ApplicationCore.Interfaces;

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
        _logger.LogInformation("Worker started at: {time}", DateTimeOffset.Now);
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