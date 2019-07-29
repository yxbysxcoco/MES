﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using SQ_DB_Framework.EFDbAccess.Config;
using SQ_DB_Framework.Entities;

using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SQ_DB_Framework
{
   public class EFDbContext : DbContext
    {

       /* public static readonly LoggerFactory MyLoggerFactory
        = new LoggerFactory(new[] { new DebugLoggerProvider()
        });*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            //.UseLoggerFactory(MyLoggerFactory)
            .UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.0.109:1521/ORCL");
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entities = Assembly.GetExecutingAssembly().GetTypes().
                Where(t => t.BaseType == typeof(EntityBase));
            foreach(var entityType in entities)
            {
                modelBuilder.Entity(entityType);
            }
            modelBuilder.ApplyConfiguration(new ToolEquipmentConfig());
            
            /*modelBuilder.Entity<Order>()
               .HasOne(o => o.customer)
               .WithMany(c=>c.Orders)
               .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<ReturnMoney>()
               .HasOne(r => r.Order)
               .WithMany(o => o.ReturnMoneys)
               .HasForeignKey(r => r.OrderCode);*/
        }
    }
    
}
