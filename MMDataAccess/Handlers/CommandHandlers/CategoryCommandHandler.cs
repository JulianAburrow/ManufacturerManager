
namespace MMDataAccess.Handlers.CommandHandlers;

public class CategoryCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : ICategoryCommandHandler
{
    public async Task CreateCategoryAsync(CategoryModel category)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.Categories.Add(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var categoryToDelete = await context.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);
        if (categoryToDelete is null)
            return;
        context.Categories.Remove(categoryToDelete);
        await context.SaveChangesAsync();

    }

    public async Task UpdateCategoryAsync(CategoryModel category)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var categoryToUpdate = await context.Categories.SingleOrDefaultAsync(c => c.CategoryId == category.CategoryId);
        if (categoryToUpdate is null)
            return;

        categoryToUpdate.Name = category.Name;
        await context.SaveChangesAsync();
    }
}
