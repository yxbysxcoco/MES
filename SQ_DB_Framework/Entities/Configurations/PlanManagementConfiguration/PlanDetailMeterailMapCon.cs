using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    class PlanDetailMeterailMapCon : IEntityTypeConfiguration<PlanDetailMeterailMap>
    {
        public void Configure(EntityTypeBuilder<PlanDetailMeterailMap> builder)
        {
            builder.HasKey(dpmmc => new { dpmmc.PlanCode, dpmmc.materialId });

            builder.HasOne(dpmmc => dpmmc.Material)
                .WithMany(dp => dp.PlanDetailMeterailMaps)
                .HasForeignKey(dpmmc => dpmmc.materialId);

            builder.HasOne(dpmmc => dpmmc.PlanMaintain)
                .WithMany(dp => dp.PlanDetailMeterailMaps)
                .HasForeignKey(dpmmc => dpmmc.PlanCode);
        }
    }
}
