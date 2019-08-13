using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations
{
    class OrderMaterialMapConfiguration : IEntityTypeConfiguration<OrderMaterialMap>
    {
        public void Configure(EntityTypeBuilder<OrderMaterialMap> builder)
        {
            builder.HasOne(omm => omm.Material)
                .WithMany()
                .HasForeignKey(omm => omm.MaterialId);
        }
    }
}
