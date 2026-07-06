namespace MMDataAccess.Handlers.QueryHandlers;

public class CategoryQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : ICategoryQueryHandler
{
    public bool CategoryExists(string categoryName)
    {
        using var context = manufacturerManagerContextFactory.CreateDbContext();
        return context.Categories.Any(c => c.Name == categoryName);
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
        return await context.Categories
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.CategoryId == categoryId)
            ?? throw new KeyNotFoundException($"Category with ID {categoryId} not found.");
    }        
}
