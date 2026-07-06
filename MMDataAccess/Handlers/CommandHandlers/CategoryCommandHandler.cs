
namespace MMDataAccess.Handlers.CommandHandlers;

public class CategoryCommandHandler(ManufacturerManagerContext context) : ICategoryCommandHandler
{
    public async Task CreateCategoryAsync(CategoryModel category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
    }

    public async Task DeleteCategoryAsync(int categoryId)
    {
        var categoryToDelete = context.Categories.SingleOrDefault(c => c.CategoryId == categoryId);
        if (categoryToDelete is null)
            return;
        context.Categories.Remove(categoryToDelete);
        await context.SaveChangesAsync();

    }

    public async Task UpdateCategoryAsync(CategoryModel category)
    {
        var categoryToUpdate = context.Categories.SingleOrDefault(c => c.CategoryId == category.CategoryId);
        if (categoryToUpdate is null)
            return;

        categoryToUpdate.Name = category.Name;
        await context.SaveChangesAsync();
    }
}
