using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations
{
    class WarehouseMaterialMapConfiguration : IEntityTypeConfiguration<WarehouseMaterialMap>
    {
        public void Configure(EntityTypeBuilder<WarehouseMaterialMap> builder)
        {
            builder.HasKey(omm => new { omm.MaterialId, omm.WarehouseId });

            builder.HasOne(omm => omm.Material)
                .WithMany()
                .HasForeignKey(omm => omm.MaterialId);

            builder.HasOne(omm => omm.Warehouse)
                .WithMany()
                .HasForeignKey(omm => omm.WarehouseId);
        }
    }
}
