
namespace MMDataAccess.Configuration;

public class CategoryConfiguration : IEntityTypeConfiguration<CategoryModel>
{
    public void Configure(EntityTypeBuilder<CategoryModel> builder)
    {
        builder.ToTable("Category");
        builder.HasKey(nameof(CategoryModel.CategoryId));
    }
}
