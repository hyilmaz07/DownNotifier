using Azure;
using DownNotifier.Business.Abstract;
using DownNotifier.DataAccess.Abstract;
using DownNotifier.DataAccess.Concrete;
using DownNotifier.Entities.Models;
using Hangfire;
using System;
using System.Diagnostics;
using System.Net;

namespace DownNotifier.BackgroundJob
{
    public class DownNotifierJobManager
    {
        private readonly INotifierRepository repository;
        private readonly IMailService mailService;
        public DownNotifierJobManager(ApplicationContext context, IMailService mailService)
        {
            repository = new NotifierRepository(context);
            this.mailService = mailService;
        }

        [Obsolete]
        public async Task PrepareProcess()
        {

            var jobs = await repository.GetAllAsync(new());


            foreach (var item in jobs.Where(a => a.IsDelete))
            {
                RecurringJob.RemoveIfExists(item.Name);

            }

            foreach (var item in jobs.Where(a => !a.IsDelete))
            {
                DownNotifierJobSchedule.CheckUrl(item.Name, item.Url, item.MailAddress, item.Interval);

            }

        }

        [Obsolete]
        public async Task CheckUrlProcess(string name, string url, string mail, int interval)
        {
            MailData md = new MailData(mail, "DownNotifier", name + " app was down");
     

            try
            {
                WebRequest request = WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                if (response == null || response.StatusCode != HttpStatusCode.OK)
                {

                    await mailService.SendAsync(md);
                }
            }
            catch (Exception ex)
            {
                await mailService.SendAsync(md);
            }
            DownNotifierJobSchedule.CheckUrl(name, url, mail, interval);

        }
    }
}
