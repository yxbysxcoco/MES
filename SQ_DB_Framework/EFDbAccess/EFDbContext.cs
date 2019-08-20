
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
using SQ_DB_Framework.Entities.Configurations;

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

         /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
            optionsBuilder
           //.UseLoggerFactory(MyLoggerFactory);
           //.UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.1.109:1521/ORCL");
           //.UseSqlite(@"DataSource=D:\Sqlite\Nomes.db");
           //.UseMySQL(@"Data Source=47.97.117.142;Database=SXCQMes;User ID=root;Password=E5915017CBE9769D4E4F0A878BB5FE6A.qc*sx;pooling=true;port=13306;sslmode=none;CharSet=utf8;");

        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Material>();
            var entities = Assembly.GetExecutingAssembly().GetTypes();

            foreach (var entityType in entities.Where(t => t.BaseType == typeof(EntityBase)))
            {
               modelBuilder.Entity(entityType);
            }

            var ImpEntities = entities.Where(t => t.GetInterface("IEntityTypeConfiguration`1") != null).ToList();
            foreach (Type entityConType in ImpEntities)
            {
                var methods = modelBuilder.GetType().GetMethods()[14];
                var EN = Activator.CreateInstance(entityConType);
                methods.MakeGenericMethod(entityConType.GetInterface("IEntityTypeConfiguration`1").GetGenericArguments()[0])
                    .Invoke(modelBuilder, new object[] { Activator.CreateInstance(entityConType) });
            }

        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
    
    
}
