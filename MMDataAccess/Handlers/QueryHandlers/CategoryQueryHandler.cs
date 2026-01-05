namespace MMDataAccess.Handlers.QueryHandlers;

public class CategoryQueryHandler(ManufacturerManagerContext context) : ICategoryQueryHandler
{
    public async Task<List<CategoryModel>> GetCategoriesAsync() =>
        await context.Categories
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync();
}
