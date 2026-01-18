
namespace MMDataAccess.Handlers.CommandHandlers;

public class CategoryCommandHandler(ManufacturerManagerContext context) : ICategoryCommandHandler
{
    public async Task CreateCategoryAsync(CategoryModel category, bool callSaveChanges)
    {
        context.Categories.Add(category);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int categoryId, bool callSaveChanges)
    {
        var categoryToDelete = context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
        if (categoryToDelete is null)
            return;
        context.Categories.Remove(categoryToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();

    }

    public async Task UpdateCategoryAsync(CategoryModel category, bool callSaveChanges)
    {
        var categoryToUpdate = context.Categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
        if (categoryToUpdate is null)
            return;

        categoryToUpdate.Name = category.Name;
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
