using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.EFDbAccess
{
   public class MyConnection
    {
        public static EFDbContext Context;
        public static readonly LoggerFactory MyLoggerFactory
        = new LoggerFactory(new[] { new DebugLoggerProvider()
        });

        //private static IServiceProvider _serviceProvider;
        public static void Initialize(IServiceCollection services)
        {
            services.AddDbContextPool<EFDbContext>(op =>
            {
               // op.UseLoggerFactory(MyLoggerFactory);
                op.UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.1.109:1521/ORCL");
            }, 256);
            Context = (EFDbContext)services.BuildServiceProvider().GetService(typeof(EFDbContext));
    }
    }
}
