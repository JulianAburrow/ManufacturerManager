namespace MMDataAccess.Interfaces.CommandHandlers;

public interface ICategoryCommandHandler
{
    Task CreateCategoryAsync(CategoryModel category);

    Task UpdateCategoryAsync(CategoryModel category);

    Task DeleteCategoryAsync(int categoryId);
}
