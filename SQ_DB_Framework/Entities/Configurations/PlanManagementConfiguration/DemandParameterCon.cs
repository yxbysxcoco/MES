using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities.PlanManagement;


namespace SQ_DB_Framework.Entities.Configurations.PlanManagementConfiguration
{
    public class DemandParameterConfiguration : IEntityTypeConfiguration<DemandParameter>
    {
        public void Configure(EntityTypeBuilder<DemandParameter> builder)
        {
            builder.HasOne(dp => dp.DemandSource)
                .WithMany();

            builder.HasOne(dp => dp.CalculationRange)
                .WithMany();
       
        }
    }
}
