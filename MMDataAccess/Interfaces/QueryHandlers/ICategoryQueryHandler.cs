namespace MMDataAccess.Interfaces.QueryHandlers;

public interface ICategoryQueryHandler
{
    public bool CategoryExists(string categoryName);

    Task<List<CategoryModel>> GetCategoriesAsync();

    Task<CategoryModel> GetCategoryAsync(int categoryId);
}
