using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SQ_DB_Framework.Entities.Configurations
{
    public class DateTableCon : IEntityTypeConfiguration<DateTable>
    {
        public void Configure(EntityTypeBuilder<DateTable> builder)
        {
            builder.HasOne(dt => dt.SuperiorDateTable)
                 .WithMany(sdt => sdt.SubsidiaryDateTables)
                 .HasForeignKey(dt => dt.SuperiorDateTableId);
        }
    }
}
