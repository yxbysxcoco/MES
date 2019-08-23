using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace SQ_DB_Framework.Entities.Configurations
{
    class MaterialTypeConfiguration : IEntityTypeConfiguration<MaterialType>
    {
        public void Configure(EntityTypeBuilder<MaterialType> builder)
        {
            builder.HasMany(mt => mt.Materials).WithOne(m => m.MaterialType);
            builder.HasMany(mt => mt.ChildrenMaterialTypes).WithOne(mt => mt.ParentMaterialType);
        }
    }
}
