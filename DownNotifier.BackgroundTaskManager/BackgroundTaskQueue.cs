using DownNotifier.Entities.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DownNotifier.BackgroundTaskManager
{
    public class BackgroundTaskQueue : IBackgroundTaskQueue<Notifier>
    {
        private readonly Channel<Notifier> queue;
        public BackgroundTaskQueue(IConfiguration configuration)
        {
          
            BoundedChannelOptions options = new(100)
            {
                FullMode = BoundedChannelFullMode.Wait
            };

            queue = Channel.CreateBounded<Notifier>(options);

        }

        public async ValueTask AddQueue(Notifier item)
        {
            ArgumentNullException.ThrowIfNull(item);

            await queue.Writer.WriteAsync(item);
        }

        public ValueTask<Notifier> DeQueue(CancellationToken cancellationToken)
        {
            var item = queue.Reader.ReadAsync(cancellationToken);

            return item;
        }
    }
}
