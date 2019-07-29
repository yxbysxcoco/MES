using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
using SQ_DB_Framework.Entities;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SQ_DB_Framework
{
   public class EFDbContext : DbContext
    {
        //建立连接耗时接近1秒，提前加载缓存DBContext待用
        //由于DbContext不支持并发，后续可进一步构造连接池
        private static readonly EFDbContext _dbContext;
        public static EFDbContext DbContext => _dbContext;

        static EFDbContext()
        {
            _dbContext = new EFDbContext();
        }
        private EFDbContext() { }
       /* public static readonly LoggerFactory MyLoggerFactory
        = new LoggerFactory(new[] { new DebugLoggerProvider()
        });*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            //.UseLoggerFactory(MyLoggerFactory)
            .UseOracle(@"User Id=C##SXCQ_V1;Password=Welcome2414;Data Source=192.168.1.109:1521/ORCL");
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entities = Assembly.GetExecutingAssembly().GetTypes().
                Where(t => t.BaseType == typeof(EntityBase));
            foreach(var entityType in entities)
            {
                modelBuilder.Entity(entityType);
            }

            /*modelBuilder.Entity<ToolEquipment>()
               .HasOne(p => p.Material)
               .WithMany()
               .HasForeignKey(p => p.MaterialId);
            modelBuilder.Entity<ToolEquipment>()
               .HasOne(p => p.MeterageUnit)
               .WithMany()
               .HasForeignKey(p => p.MeterageUnitId);
            modelBuilder.Entity<ToolEquipment>()
               .HasOne(p => p.MoneyUnit)
               .WithMany()
               .HasForeignKey(p => p.MoneyUnitId);
            modelBuilder.Entity<ToolEquipment>()
               .HasOne(p => p.Storehouse)
               .WithMany()
               .HasForeignKey(p => p.StorehouseId);
            modelBuilder.Entity<ToolEquipment>()
               .HasOne(p => p.ToolEquipmentType)
               .WithMany()
               .HasForeignKey(p => p.TypeId);

            modelBuilder.Entity<Order>()
               .HasOne(o => o.customer)
               .WithMany(c=>c.Orders)
               .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<ReturnMoney>()
               .HasOne(r => r.Order)
               .WithMany(o => o.ReturnMoneys)
               .HasForeignKey(r => r.OrderCode);*/
        }
    }
   /* public class ToolEquipmentMap : EntityMappingConfiguration<ToolEquipment>
    {
        public override void Map(EntityTypeBuilder<ToolEquipment> t)
        {
            t.ToTable("ToolEquipment");
            t.HasKey(T => T.Code);
            t.HasOne(p => p.Material)
               .WithMany()
               .HasForeignKey(p => p.MaterialId);
        }
    }*/
}
