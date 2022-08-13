using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ManufacturerManager.DataAccess
{
    public class WidgetStatusConfiguration : IEntityTypeConfiguration<WidgetStatus>
    {
        public void Configure(EntityTypeBuilder<WidgetStatus> builder)
        {
            builder.Property(e => e.StatusName)
                .IsUnicode(false);
            builder.HasMany(e => e.Widget)
                .WithOne(e => e.WidgetStatus)
                .IsRequired(true)
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
