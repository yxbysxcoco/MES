using Microsoft.EntityFrameworkCore;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    public class WorkOrderCon : IEntityTypeConfiguration<WorkOrder>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<WorkOrder> builder)
        {
            builder.HasOne(woc => woc.PlanMaintain)
                .WithMany();

            builder.HasOne(woc => woc.Status)
                .WithMany();

            builder.HasOne(woc => woc.Technological)
                .WithMany();

        }
    }
}
