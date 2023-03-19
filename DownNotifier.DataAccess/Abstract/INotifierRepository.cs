using DownNotifier.Core.DataAccess;
using DownNotifier.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.DataAccess.Abstract
{
    public interface INotifierRepository:IRepositoryBase<Notifier>
    {
    }
}
