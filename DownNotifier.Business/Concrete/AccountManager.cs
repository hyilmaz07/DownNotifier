using DownNotifier.Business.Abstract;
using DownNotifier.Core.Helper;
using DownNotifier.DataAccess.Abstract;
using DownNotifier.DataAccess.Concrete;
using DownNotifier.Entities.Models;

namespace DownNotifier.Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountRepository repository;
        public AccountManager(ApplicationContext context)
        {
            repository = new AccountRepository(context);
        }

        public async Task<Account> AddAsync(Account entity)
        {
            return await repository.AddAsync(entity);
        }

        public async Task<bool> DeleteAsync(Account entity)
        {
            return await repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<Account>> GetAllAsync(GenericExpression<Account> parameters = null)
        {
            return await repository.GetAllAsync(parameters);
        }

        public async Task<Account> GetAsync(GenericExpression<Account> parameters = null)
        {
            return await repository.GetAsync(parameters);
        } 

        public async Task<Account> UpdateAsync(Account entity)
        {
            return await repository.UpdateAsync(entity);
        }
    }
}
