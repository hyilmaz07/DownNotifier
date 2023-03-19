using Microsoft.Extensions.Hosting;

namespace DownNotifier.DownNotifierJobManager
{
    public class PeriodicHostedService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}