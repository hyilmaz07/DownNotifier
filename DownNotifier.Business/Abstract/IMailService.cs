using DownNotifier.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Business.Abstract
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData);
    }
}
