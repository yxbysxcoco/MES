﻿using Microsoft.EntityFrameworkCore;
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
                op.UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.1.121:1521/ORCL");
                //op.UseSqlite(@"DataSource=D:\Sqlite\Nomes.db");
               // op.UseMySQL(@"Data Source=47.97.117.142;Database=SXCQ_Mes;User ID=root;Password=E5915017CBE9769D4E4F0A878BB5FE6A.qc*sx;pooling=true;port=13306;sslmode=none;CharSet=utf8;");

            }, 1).AddTransient<EFDbContext>();

            ServiceProvider = services.BuildServiceProvider();
            
        }
        public static EFDbContext GetEFDbContext()
        {
            return ServiceProvider.GetService<EFDbContext>();
        }

    }
    
}
