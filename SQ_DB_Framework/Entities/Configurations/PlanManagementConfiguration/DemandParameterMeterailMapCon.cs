using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    public class DemandParameterMeterailMapCon : IEntityTypeConfiguration<DemandParameterMeterailMap>
    {
        public void Configure(EntityTypeBuilder<DemandParameterMeterailMap> builder)
        {
            builder.HasKey(dpmmc => new { dpmmc.DemandParameterId, dpmmc.materialId });

            builder.HasOne(dpmmc => dpmmc.Material)
                .WithMany(dp => dp.DemandParameterMeterailMaps)
                .HasForeignKey(dpmmc => dpmmc.materialId);

            builder.HasOne(dpmmc => dpmmc.DemandParameter)
                .WithMany(dp => dp.DemandParameterMeterailMap)
                .HasForeignKey(dpmmc => dpmmc.DemandParameterId);
        }
    }
}
