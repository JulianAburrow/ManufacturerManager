namespace MMDataAccess.Handlers.QueryHandlers;

public class CategoryQueryHandler(ManufacturerManagerContext context) : ICategoryQueryHandler
{
    public bool CategoryExists(string categoryName) =>
        context.Categories.Any(c => c.Name == categoryName);

    public async Task<List<CategoryModel>> GetCategoriesAsync() =>
        await context.Categories
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync();

    public async Task<CategoryModel> GetCategoryAsync(int categoryId) =>
        await context.Categories
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.CategoryId == categoryId)
            ?? throw new ArgumentNullException(nameof(categoryId), "Category not found");
}
