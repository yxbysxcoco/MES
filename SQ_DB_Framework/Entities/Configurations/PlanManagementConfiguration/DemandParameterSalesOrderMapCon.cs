using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    public class DemandParameterSalesOrderMapCon : IEntityTypeConfiguration<DemandParameterSalesOrderMap>
    {
        public void Configure(EntityTypeBuilder<DemandParameterSalesOrderMap> builder)
        {
            builder.HasKey(dpsomc => new { dpsomc.DemandParameterId, dpsomc.OrderCode });

            builder.HasOne(dpsomc => dpsomc.SalesOrder)
                .WithMany(dp => dp.DemandParameterSalesOrderMaps)
                .HasForeignKey(dpsomc => dpsomc.OrderCode);

            builder.HasOne(dpsomc => dpsomc.DemandParameter)
                .WithMany(dp => dp.DemandParameterSalesOrderMaps)
                .HasForeignKey(dpsomc => dpsomc.DemandParameterId);
        }
    }
}
