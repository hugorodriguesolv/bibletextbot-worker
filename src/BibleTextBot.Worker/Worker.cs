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
        _botTextoBiblicoService.GetBibleTextAsync();

        //while (!stoppingToken.IsCancellationRequested)
        //{
        //    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //    await Task.Delay(1000, stoppingToken);
        //}
    }
}