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
        public List<Table.ToolEquipment> FindUpcomingDinners(Models.ToolEquipment toolEquipmentParam)
        {
            var toolEquipments = new List<Table.ToolEquipment>();
            var query = from t in db.ToolEquipment
                       join material in db.Material on t.MaterialId equals material.MaterialId
                       join meterageUnit in db.MeterageUnit on t.MeterageUnitId equals meterageUnit.MeterageUnitId
                       join moneyUnit in db.MoneyUnit on t.MoneyUnitId equals moneyUnit.MoneyUnitId
                       join storehouse in db.Storehouse on t.StorehouseId equals storehouse.StorehouseId
                       join type in db.ToolEquipmentType on t.TypeId equals type.TypeId
                       where t.DateAdded < DateTime.Now
                       orderby t.DateAdded
                       select new { materialName=material.Name, meterageUnitName=meterageUnit.Name,
                           moneyUnitName = moneyUnit.Name,
                           storehouseName = storehouse.Name,
                           TypeName = type.Name,
                           toolEquipment = t,
                       };
            foreach (var item in query)
            {
                Table.ToolEquipment toolEquipment = new Table.ToolEquipment
                {
                    Code = item.toolEquipment.Code,
                    Name = item.toolEquipment.Name,
                    Edition = item.toolEquipment.Edition,
                    Standard = item.toolEquipment.Standard,
                    MaterialId = item.toolEquipment.MaterialId,
                    Weight = item.toolEquipment.Weight,
                    Mark = item.toolEquipment.Mark,
                    Remark = item.toolEquipment.Remark,
                    MeterageUnitId = item.toolEquipment.MeterageUnitId,
                    MoneyUnitId = item.toolEquipment.MoneyUnitId,
                    Univalence = item.toolEquipment.Univalence,
                    LowestStock = item.toolEquipment.LowestStock,
                    HighestStock = item.toolEquipment.HighestStock,
                    SaveStock = item.toolEquipment.SaveStock,
                    StorehouseId = item.toolEquipment.StorehouseId,
                    Manufacturer = item.toolEquipment.Manufacturer,
                    DateAdded = item.toolEquipment.DateAdded,
                    ExitNumber = item.toolEquipment.ExitNumber,
                    InspectionCompany = item.toolEquipment.InspectionCompany,
                    MaxUseTime = item.toolEquipment.MaxUseTime,
                    RepairCycle = item.toolEquipment.RepairCycle,
                    Supplier = item.toolEquipment.Supplier,
                    MoneyUnitName = item.moneyUnitName,
                    MeterageUnitName = item.meterageUnitName,
                    MaterialName = item.materialName,
                    StorehouseName = item.storehouseName,
                    TypeName = item.TypeName
                };
                toolEquipments.Add(toolEquipment);
            }

            return toolEquipments;
        }
    }
}
