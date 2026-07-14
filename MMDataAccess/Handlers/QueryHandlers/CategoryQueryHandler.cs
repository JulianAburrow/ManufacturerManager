namespace MMDataAccess.Handlers.QueryHandlers;

public class CategoryQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : ICategoryQueryHandler
{
    public async Task<bool> CategoryExistsAsync(string categoryName)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Categories.AnyAsync(c => c.Name == categoryName);
    }

    public async Task<List<CategoryModel>> GetCategoriesAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Categories
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CategoryModel> GetCategoryAsync(int categoryId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var category = await context.Categories
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.CategoryId == categoryId);

        return category ?? new CategoryModel(); ;
    }        
}
