using DownNotifier.Entities.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DownNotifier.BackgroundTaskManager
{
    public class PeriodicHostedService : BackgroundService
    {
        private readonly ILogger<PeriodicHostedService> logger;
        private readonly IBackgroundTaskQueue<Notifier> queue;

        public PeriodicHostedService(ILogger<PeriodicHostedService> logger, IBackgroundTaskQueue<Notifier> queue)
        {
            this.logger = logger;
            this.queue = queue;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var job = await queue.DeQueue(stoppingToken); 

                await Task.Delay(1000);//db işlemini simüle etmek için 1 saniye beklettik
                logger.LogInformation($"ExecuteAsync worked for {job.Name}");
            }
        }
    }
}