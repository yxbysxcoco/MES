using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    class ProductionDemandAnalysisCon : IEntityTypeConfiguration<ProductionDemandAnalysis>
    {
        public void Configure(EntityTypeBuilder<ProductionDemandAnalysis> builder)
        {
            builder.HasOne(pdac => pdac.ProductionDemandScheme)
                .WithMany();
        }
    }
}
