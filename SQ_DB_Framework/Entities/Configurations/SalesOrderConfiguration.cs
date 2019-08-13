using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations
{
    class OrderConfiguration : IEntityTypeConfiguration<SalesOrder>
    {
        public void Configure(EntityTypeBuilder<SalesOrder> builder)
        {
            builder.HasMany(so => so.OrderMaterialMaps)
                .WithOne()
                .HasForeignKey(omm => omm.OrderCode);

            builder.HasMany(so => so.ReturnMoneys)
                .WithOne(rm => rm.Order)
                .HasForeignKey(rm => rm.OrderCode);

        }
    }
}
