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

    public class ToolEquipmentRepository
    {
        private readonly DateBaseContext db = new DateBaseContext();
        public List<Models.ToolEquipment> FindUpcomingDinners(Models.ToolEquipment toolEquipmentParam)
        {
            //IQueryable<Models.ToolEquipment>
            var toolEquipments = new List<Models.ToolEquipment>();
            var query = from t in db.ToolEquipment.AsQueryable()
                        join material in db.Material on t.MaterialId equals material.MaterialId
                        join meterageUnit in db.MeterageUnit on t.MeterageUnitId equals meterageUnit.MeterageUnitId
                        join moneyUnit in db.MoneyUnit on t.MoneyUnitId equals moneyUnit.MoneyUnitId
                        join storehouse in db.Storehouse on t.StorehouseId equals storehouse.StorehouseId
                        join type in db.ToolEquipmentType on t.TypeId equals type.TypeId
                        where t.DateAdded < DateTime.Now
                        orderby t.DateAdded
                        //select t;
                       select new { materialName=material.Name, meterageUnitName=meterageUnit.Name,
                           moneyUnitName = moneyUnit.Name,
                           storehouseName = storehouse.Name,
                           TypeName = type.Name,
                           toolEquipment = t,
                       };
            foreach (var item in query)
            {
                Models.ToolEquipment toolEquipment = item.toolEquipment;
                toolEquipment.MoneyUnitName = item.moneyUnitName;
                toolEquipment.MeterageUnitName = item.meterageUnitName;
                toolEquipment.MaterialName = item.materialName;
                toolEquipment.StorehouseName = item.storehouseName;
                toolEquipment.TypeName = item.TypeName;
                toolEquipments.Add(toolEquipment);
            }
            return toolEquipments;
        }

    }
}
