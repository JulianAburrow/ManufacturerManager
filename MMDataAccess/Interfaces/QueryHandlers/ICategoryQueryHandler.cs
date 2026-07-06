namespace MMDataAccess.Interfaces.QueryHandlers;

public interface ICategoryQueryHandler
{
    public Task<bool> CategoryExistsAsync(string categoryName);

    Task<List<CategoryModel>> GetCategoriesAsync();

    Task<CategoryModel> GetCategoryAsync(int categoryId);
}
