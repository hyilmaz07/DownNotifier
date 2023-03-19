using DownNotifier.Core;
using DownNotifier.Core.DataAccess;
using DownNotifier.DataAccess.Abstract;
using DownNotifier.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.DataAccess.Concrete
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
