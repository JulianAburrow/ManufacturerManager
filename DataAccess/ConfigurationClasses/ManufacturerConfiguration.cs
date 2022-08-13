using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManufacturerManager.DataAccess
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.Property(e => e.Name)
                .IsUnicode(false);
            builder.HasMany(e => e.Widget)
                .WithOne(e => e.Manufacturer)
                .HasForeignKey(e => e.ManufacturerId)
                .IsRequired(true);
            builder.HasOne(e => e.ManufacturerStatus)
                .WithMany(e => e.Manufacturer)
                .HasForeignKey(e => e.StatusId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
