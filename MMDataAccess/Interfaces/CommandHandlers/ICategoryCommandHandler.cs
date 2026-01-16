namespace MMDataAccess.Interfaces.CommandHandlers;

public interface ICategoryCommandHandler
{
    Task CreateCategoryAsync(CategoryModel category, bool callSaveChanges);

    Task UpdateCategoryAsync(CategoryModel category, bool callSaveChanges);

    Task DeleteCategoryAsync(int categoryId, bool callSaveChanges);

    Task SaveChangesAsync();
}
