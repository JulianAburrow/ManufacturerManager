namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IMyMMStatusQueryHandler
{
    Task<List<MyMMStatusModel>> GetMMStatusesAsync();

    Task<MyMMStatusModel> GetMyMMStatusAsync(int myMMStatusId);
}
