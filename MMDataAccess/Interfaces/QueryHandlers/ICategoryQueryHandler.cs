namespace MMDataAccess.Interfaces.QueryHandlers;

public interface ICategoryQueryHandler
{
    Task<List<CategoryModel>> GetCategoriesAsync();
}
