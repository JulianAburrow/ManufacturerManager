
namespace MMDataAccess.Configuration;

public class MyMMStatusConfiguration : IEntityTypeConfiguration<MyMMStatusModel>
{
    public void Configure(EntityTypeBuilder<MyMMStatusModel> builder)
    {
        builder.ToTable("MyMMStatus");
        builder.HasKey(nameof(MyMMStatusModel.StatusId));
        builder.HasMany(s => s.MyMMs)
               .WithOne(m => m.Status)
               .HasForeignKey(m => m.StatusId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}
