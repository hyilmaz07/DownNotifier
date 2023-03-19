using DownNotifier.Business.Abstract;
using DownNotifier.Core.Helper;
using DownNotifier.DataAccess.Abstract;
using DownNotifier.DataAccess.Concrete;
using DownNotifier.Entities.Models;

namespace DownNotifier.Business.Concrete
{
    public class NotifierManager : INotifierService
    {
        private readonly INotifierRepository repository;
        public NotifierManager(ApplicationContext context)
        {
            repository = new NotifierRepository(context);
        }

        public async Task<Notifier> AddAsync(Notifier entity)
        {
            return await repository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Notifier entity)
        {
            return await repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Notifier>> GetAllAsync(GenericExpression<Notifier> parameters = null)
        {
            return await repository.GetAllAsync(parameters);
        }

        public async Task<Notifier> GetAsync(GenericExpression<Notifier> parameters = null)
        {
            return await repository.GetAsync(parameters);
        }

        public async Task<Notifier> UpdateAsync(Notifier entity)
        {
            return await repository.UpdateAsync(entity);
        }
    }
}
