using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SQ_DB_Framework.EFDbAccess
{
   public class MyConnection
    {
        public static readonly LoggerFactory MyLoggerFactory
        = new LoggerFactory(new[] { new DebugLoggerProvider()
        });

        public static IServiceProvider ServiceProvider { get; set; }

        public static void Initialize(IServiceCollection services)
        {
            services.AddDbContextPool<EFDbContext>(op =>
             {
                 // op.UseLoggerFactory(MyLoggerFactory);
                 op.UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.1.109:1521/ORCL");
                 //op.UseSqlite(@"DataSource=D:\Sqlite\Nomes.db");

             }, 1).AddTransient<EFDbContext>();

            ServiceProvider = services.BuildServiceProvider();
            
        }
        public static EFDbContext getEFDbContext()
        {
            return ServiceProvider.GetService<EFDbContext>();
        }

       public static void test(ModelBuilder modelBuilder)
        {
            var entities = Assembly.GetExecutingAssembly().GetTypes();
            var ImpEntities = entities.Where(t => t.GetInterface("IEntityTypeConfiguration`1") != null).ToList();
            foreach (Type entityConType in ImpEntities)
            {
                var methods = modelBuilder.GetType().GetMethods()[14];
                var EN = Activator.CreateInstance(entityConType);
                methods.MakeGenericMethod(entityConType.GetInterface("IEntityTypeConfiguration`1").GetGenericArguments()[0]).Invoke(modelBuilder, new object[] { Activator.CreateInstance(entityConType) });
               // modelBuilder.GetType().InvokeMember("ApplyConfiguration", BindingFlags.InvokeMethod, null, modelBuilder,
               //new object[] { Activator.CreateInstance(entityType) });
            }
        }
        
    }
    
}
