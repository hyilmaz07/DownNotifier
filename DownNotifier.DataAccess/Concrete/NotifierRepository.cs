using DownNotifier.Core;
using DownNotifier.Core.DataAccess;
using DownNotifier.DataAccess.Abstract;
using DownNotifier.Entities.Models;

namespace DownNotifier.DataAccess.Concrete
{
    public class NotifierRepository : RepositoryBase<Notifier>, INotifierRepository
    {
        public NotifierRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
