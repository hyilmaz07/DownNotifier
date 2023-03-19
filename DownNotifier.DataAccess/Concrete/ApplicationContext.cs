using DownNotifier.Core;
using DownNotifier.Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace DownNotifier.DataAccess.Concrete
{
    public   class ApplicationContext : BaseContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor httpContextAccessor) : base(options, httpContextAccessor)
        {
                
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Notifier> Notifier { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Seed();
            base.OnModelCreating(builder);
        }
    }
}
