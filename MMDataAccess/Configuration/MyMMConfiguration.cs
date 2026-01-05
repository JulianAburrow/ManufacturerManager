namespace MMDataAccess.Configuration;

public class MyMMConfiguration : IEntityTypeConfiguration<MyMMModel>
{
    public void Configure(EntityTypeBuilder<MyMMModel> builder)
    {
        
        builder.ToTable("MyMM");
        builder.HasKey(nameof(MyMMModel.MyMMId));
        builder.HasOne(m => m.Status)
               .WithMany(s => s.MyMMs)
               .HasForeignKey(m => m.StatusId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);
    }
}
