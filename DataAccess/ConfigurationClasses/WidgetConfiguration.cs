using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManufacturerManager.DataAccess
{
    public class WidgetConfiguration : IEntityTypeConfiguration<Widget>
    {
        public void Configure(EntityTypeBuilder<Widget> builder)
        {
            builder.Property(e => e.Name)
                .IsUnicode(false);
            builder.HasOne(e => e.Manufacturer)
                .WithMany(e => e.Widget)
                .HasForeignKey(e => e.ManufacturerId)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(e => e.Colour)
                .WithMany(e => e.Widget)
                .HasForeignKey(e => e.ColourId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
