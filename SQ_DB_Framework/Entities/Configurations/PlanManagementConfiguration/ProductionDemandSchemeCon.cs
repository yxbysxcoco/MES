using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;
using System;


namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    class ProductionDemandSchemeConfiguration : IEntityTypeConfiguration<ProductionDemandScheme>
    {
        public void Configure(EntityTypeBuilder<ProductionDemandScheme> builder)
        {
          

            builder.HasOne(pds => pds.Employee)
                .WithMany();

            builder.HasOne(pds => pds.DemandParameter)
                .WithMany();

            builder.HasOne(pds => pds.RunParameter)
                .WithMany();

            builder.HasOne(pds => pds.CalculationParameter)
                .WithMany();

        }
    }
}
