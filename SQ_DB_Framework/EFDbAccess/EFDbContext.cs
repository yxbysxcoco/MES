
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using SQ_DB_Framework.EFDbAccess.Config;
using SQ_DB_Framework.Entities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace SQ_DB_Framework
{
   public class EFDbContext : DbContext
    {

      public EFDbContext(DbContextOptions<EFDbContext> options) : base(options)
        {
        }
        /* public static readonly LoggerFactory MyLoggerFactory
         = new LoggerFactory(new[] { new DebugLoggerProvider()
         });*/

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* optionsBuilder
             //.UseLoggerFactory(MyLoggerFactory);
              .UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.1.109:1521/ORCL");*/
           

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entities = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var entityType in entities.Where(t => t.BaseType == typeof(EntityBase)))
            {
                modelBuilder.Entity(entityType);
            }

            foreach (var entityType in entities.Where(t => t.BaseType == typeof(IEntityTypeConfiguration<>)))
            {
                dynamic configurationInstance = Activator.CreateInstance(entityType);
                modelBuilder.ApplyConfiguration(new ToolEquipmentConfig());
            }

            /*modelBuilder.Entity<Order>()
               .HasOne(o => o.customer)
               .WithMany(c=>c.Orders)
               .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<ReturnMoney>()
               .HasOne(r => r.Order)
               .WithMany(o => o.ReturnMoneys)
               .HasForeignKey(r => r.OrderCode);*/
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
    
    
}
