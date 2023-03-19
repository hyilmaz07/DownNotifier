using DownNotifier.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifier.DataAccess.Concrete
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                 .HasData(new Account()
                 {
                     Id=1,
                     Name = "AcerPro",
                     MailAddress = "info@acerpro.com.tr",
                     Password = "123",
                     IsDelete=false
                 });
        }
    }
}
