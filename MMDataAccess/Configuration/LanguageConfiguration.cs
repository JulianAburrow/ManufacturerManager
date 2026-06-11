namespace MMDataAccess.Configuration;

public class LanguageConfiguration : IEntityTypeConfiguration<LanguageModel>
{
    public void Configure(EntityTypeBuilder<LanguageModel> builder)
    {
        builder.ToTable("Language");
        builder.HasKey(l => l.LanguageId);
    }
}
