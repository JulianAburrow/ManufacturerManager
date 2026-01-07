namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IMyMMQueryHandler
{
    Task<MyMMModel> GetMyMMAsync(int myMMId);
    
    Task<List<MyMMModel>> GetMyMMsAsync();

    Task<List<MyMMModel>> GetMyMMsForHomePageAsync();
}
