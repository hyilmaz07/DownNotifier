using DownNotifier.Core.Helper;
using DownNotifier.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.Business.Abstract
{
    public interface IAccountService
    {
        Task<Account> AddAsync(Account entity);
        Task<Account> UpdateAsync(Account entity);
        Task<bool> DeleteAsync(Account entity);
        Task<Account> GetAsync(GenericExpression<Account> parameters = null);
        Task<IEnumerable<Account>> GetAllAsync(GenericExpression<Account> parameters = null);
    }
}
