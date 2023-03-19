using DownNotifier.Core.Helper;
using DownNotifier.Entities.Models;

namespace DownNotifier.Business.Abstract
{
    public interface INotifierService
    {
        Task<Notifier> AddAsync(Notifier entity);
        Task<Notifier> UpdateAsync(Notifier entity);
        Task<bool> DeleteAsync(Notifier entity);
        Task<Notifier> GetAsync(GenericExpression<Notifier> parameters = null);
        Task<IEnumerable<Notifier>> GetAllAsync(GenericExpression<Notifier> parameters = null);
    }
}
