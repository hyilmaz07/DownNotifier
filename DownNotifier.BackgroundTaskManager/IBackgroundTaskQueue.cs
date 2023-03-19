using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.BackgroundTaskManager
{
    public interface IBackgroundTaskQueue<T>
    {
        ValueTask AddQueue(T item);
        ValueTask<T> DeQueue(CancellationToken cancellationToken);
    }
}
