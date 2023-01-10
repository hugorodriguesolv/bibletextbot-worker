namespace BibleTextBot.Worker;

using ApplicationCore.Interfaces;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBotBibleTextService _botTextoBiblicoService;

    public Worker(ILogger<Worker> logger, IBotBibleTextService botTextoBiblicoService)
    {
        _logger = logger;
        _botTextoBiblicoService = botTextoBiblicoService;
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