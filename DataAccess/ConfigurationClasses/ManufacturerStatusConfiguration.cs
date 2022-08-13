using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManufacturerManager.DataAccess
{
    public class ManufacturerStatusConfiguration : IEntityTypeConfiguration<ManufacturerStatus>
    {
        public void Configure(EntityTypeBuilder<ManufacturerStatus> builder)
        {
            builder.Property(e => e.StatusName)
                .IsUnicode(false);
            builder.HasMany(e => e.Manufacturer)
                .WithOne(e => e.ManufacturerStatus)
                .HasForeignKey(e => e.StatusId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
