using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations
{
    class WarehouseDepartmentMapConfiguration : IEntityTypeConfiguration<WarehouseDepartmentMap>
    {
        public void Configure(EntityTypeBuilder<WarehouseDepartmentMap> builder)
        {
            builder.HasKey(omm => new { omm.DepartmentId, omm.WarehouseId });

            builder.HasOne(omm => omm.Department)
                .WithMany()
                .HasForeignKey(omm => omm.DepartmentId);

            builder.HasOne(omm => omm.Warehouse)
                .WithMany()
                .HasForeignKey(omm => omm.WarehouseId);
        }
    }
}
