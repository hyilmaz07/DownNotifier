using DownNotifier.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DownNotifier.Core
{
    public class BaseContext : DbContext
    {
        private readonly IHttpContextAccessor HttpContextAccessor;
        public BaseContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        public override int SaveChanges()
        {
            UpdateAuditEntities(); 
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditEntities(); 
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditEntities(); 
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditEntities()
        {
            string CurrentUserId = GetCurrenctUser();

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is EntityBase &&
                            (x.State == EntityState.Added || x.State == EntityState.Modified));


            foreach (var entry in modifiedEntries)
            {

                var entity = (EntityBase)entry.Entity;
                var now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedDate = now;
                    entity.CreatedBy = CurrentUserId;
                }
                else if (entry.State == EntityState.Deleted )
                { 
                    entry.State = EntityState.Unchanged;
                     
                    entity.IsDelete = true;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedDate = now;
                entity.UpdatedBy = CurrentUserId;
            }
        }
 
        private string GetCurrenctUser()
        {
            if (HttpContextAccessor.HttpContext?.User?.Identity == null) return null;

            var claims = (ClaimsIdentity)HttpContextAccessor.HttpContext.User.Identity;

            string CurrentName = claims?.Claims?.FirstOrDefault(a => a.Type.Contains("/name"))?.Value ?? null; 

   

            return CurrentName;
        }
    }
}
