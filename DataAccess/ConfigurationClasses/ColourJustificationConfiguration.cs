
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManufacturerManager.DataAccess
{
    public class ColourJustificationConfiguration : IEntityTypeConfiguration<ColourJustification>
    {
        public void Configure(EntityTypeBuilder<ColourJustification> builder)
        {
            builder.Property(e => e.Justification)
                .IsUnicode(false);
            builder.HasMany(e => e.Widget)
                .WithOne(e => e.ColourJustification)
                .HasForeignKey(e => e.ColourJustificationId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
