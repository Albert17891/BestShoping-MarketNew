using NCrontab;

namespace MarketPlace.CheckDiscountWorker;

public class DiscountWorker : BackgroundService
{
    private readonly ILogger<DiscountWorker> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly CrontabSchedule _schedule;
    private DateTime _nextRun;

    private string Schedule => "0 */1 * * * *";
    public DiscountWorker(ILogger<DiscountWorker> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _schedule = CrontabSchedule.Parse(Schedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
        _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        do
        {
            var now = DateTime.Now;
            var nextRun = _schedule.GetNextOccurrence(now);
            if (nextRun > _nextRun)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var service = scope.ServiceProvider.GetRequiredService<DiscountCheckClient>();

                    await service.CheckDiscount();

                    _logger.LogInformation("Send Request");

                }
                _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
            }

        } while (!stoppingToken.IsCancellationRequested);
    }
}

