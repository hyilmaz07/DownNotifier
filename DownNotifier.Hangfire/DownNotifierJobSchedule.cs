using DownNotifier.DataAccess.Abstract;
using DownNotifier.DataAccess.Concrete;
using DownNotifier.Entities.Models;
using Hangfire;
using System.Diagnostics;

namespace DownNotifier.BackgroundJob
{
    public static class DownNotifierJobSchedule
    {
        [Obsolete]
        public static async void PrepareJobs()
        {
            Hangfire.BackgroundJob.Enqueue<DownNotifierJobManager>(
                job => job.PrepareProcess());
        }

        [Obsolete]
        public static async void CheckUrl(string name, string url, string mail, int interval)
        {
            RecurringJob.RemoveIfExists(name);
            Debug.WriteLine(name + " removed " + DateTime.Now);
            await Task.Delay(500);
            RecurringJob.AddOrUpdate<DownNotifierJobManager>(name,
                  job => job.CheckUrlProcess(name, url,mail, interval)
                  , Cron.MinuteInterval(interval)
                );
        }
    }
}
