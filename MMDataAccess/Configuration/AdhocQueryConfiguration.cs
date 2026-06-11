namespace MMDataAccess.Configuration;

public class AdhocQueryConfiguration : IEntityTypeConfiguration<AdhocQueryModel>
{
    public void Configure(EntityTypeBuilder<AdhocQueryModel> builder)
    {
        builder.ToTable("AdhocQuery");
        builder.HasKey(a => a.AdhocQueryId);
    }
}
