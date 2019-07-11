using MES.Const;
using MES.Models;
using MES.Table;
using MES.Tools;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MES.Config
{
    public class DateBaseContext : DbContext
    {
        public DbSet<Models.Storehouse> Storehouse { get; set; }
        public DbSet<Models.ToolEquipment> ToolEquipment { get; set; }
        public DbSet<Models.Material> Material { get; set; }
        public DbSet<Models.MeterageUnit> MeterageUnit { get; set; }
        public DbSet<Models.MoneyUnit> MoneyUnit { get; set; }
        public DbSet<Models.ToolEquipmentType> ToolEquipmentType { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //需下载Oracle.EntityFrameworkCore包
            optionsBuilder.UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.0.109:1521/ORCL");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Storehouse>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.ToolEquipment>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.Material>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.MeterageUnit>()
                .HasAlternateKey(c => c.Name);
            modelBuilder.Entity<Models.ToolEquipmentType>()
                .HasAlternateKey(c => c.Name);
        }
    }
}
