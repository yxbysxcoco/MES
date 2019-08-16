using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    public class PlanMaintainCon : IEntityTypeConfiguration<PlanMaintain>
    {
        public void Configure(EntityTypeBuilder<PlanMaintain> builder)
        {
            builder.HasOne(pmc => pmc.PlanType)
                .WithMany();

            builder.HasOne(pmc => pmc.Department)
                .WithMany();

            builder.HasOne(pmc => pmc.Status)
                .WithMany();

            builder.HasOne(pmc => pmc.PlanAccessories)
                .WithMany();
        }
    }
}
