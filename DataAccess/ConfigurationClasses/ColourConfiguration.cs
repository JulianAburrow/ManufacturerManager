using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManufacturerManager.DataAccess
{
    public class ColourConfiguration : IEntityTypeConfiguration<Colour>
    {
        public void Configure(EntityTypeBuilder<Colour> builder)
        {
            builder.Property(e => e.Name)
                .IsUnicode(false);
            builder.HasMany(e => e.Widget)
                .WithOne(e => e.Colour)
                .IsRequired(false)
                .HasForeignKey(e => e.ColourId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
