using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManufacturerManager.DataAccess
{
    public class StaffMemberConfiguration : IEntityTypeConfiguration<StaffMember>
    {
        public void Configure(EntityTypeBuilder<StaffMember> builder)
        {
            builder.Property(e => e.FirstName)
                .IsUnicode(false);
            builder.Property(e => e.LastName)
                .IsUnicode(false);
            #region Manufacturer
            builder.HasMany(e => e.ManufacturerCreated)
                .WithOne(e => e.StaffMemberCreated)
                .HasForeignKey(e => e.CreatedById)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.ManufacturerUpdated)
                .WithOne(e => e.StaffMemberUpdated)
                .HasForeignKey(e => e.LastUpdatedById)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
            #region Widget
            builder.HasMany(e => e.WidgetCreated)
                .WithOne(e => e.StaffMemberCreated)
                .HasForeignKey(e => e.CreatedById)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(e => e.WidgetUpdated)
                .WithOne(e => e.StaffMemberUpdated)
                .HasForeignKey(e => e.LastUpdatedById)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.NoAction);
            #endregion
        }
    }
}
