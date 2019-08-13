using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities;

namespace SQ_DB_Framework.EFDbAccess.Config
{
    public class ToolEquipmentConfig : IEntityTypeConfiguration<ToolEquipment>
    {

        public void Configure(EntityTypeBuilder<ToolEquipment> t)
        {
            t.ToTable("ToolEquipment");
            t.HasKey(T => T.Code);
            t.HasOne(p => p.Material)
              .WithMany()
              .HasForeignKey(p => p.MaterialId);
            t.HasOne(p => p.MeterageUnit)
              .WithMany()
              .HasForeignKey(p => p.MeterageUnitId);
            t.HasOne(p => p.MoneyUnit)
              .WithMany()
              .HasForeignKey(p => p.MoneyUnitId);
            t.HasOne(p => p.Storehouse)
              .WithMany()
              .HasForeignKey(p => p.StorehouseId);
            t.HasOne(p => p.ToolEquipmentType)
              .WithMany()
              .HasForeignKey(p => p.TypeId);
        }
    }
}
