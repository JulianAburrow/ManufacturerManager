namespace MMDataAccess.Handlers.QueryHandlers;

public class CategoryQueryHandler(ManufacturerManagerContext context) : ICategoryQueryHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task<List<CategoryModel>> GetCategoriesAsync() =>
        await _context.Categories
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync();
}
