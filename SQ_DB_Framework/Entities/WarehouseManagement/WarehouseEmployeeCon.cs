using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations
{
    class WarehouseEmployeeMapConfiguration : IEntityTypeConfiguration<WarehouseEmployeeMap>
    {
        public void Configure(EntityTypeBuilder<WarehouseEmployeeMap> builder)
        {
            builder.HasKey(omm => new { omm.EmployeeId, omm.WarehouseId });

            builder.HasOne(omm => omm.Employee)
                .WithMany()
                .HasForeignKey(omm => omm.EmployeeId);

            builder.HasOne(omm => omm.Warehouse)
                .WithMany()
                .HasForeignKey(omm => omm.WarehouseId);
        }
    }
}
