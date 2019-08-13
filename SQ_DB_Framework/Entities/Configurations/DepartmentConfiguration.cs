using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SQ_DB_Framework.Entities;

namespace SQ_DB_Framework.Entities.Configurations
{
    class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasMany(dp => dp.Employees)
                .WithOne(emp => emp.Department)
                .HasForeignKey(emp => emp.DepartmentId);

            builder.HasOne(dp => dp.SuperiorDepartment)
                .WithMany(sdp => sdp.SubsidiaryDepartments)
                .HasForeignKey(dp => dp.SuperiorDepartmentId);
        }
    }
}
