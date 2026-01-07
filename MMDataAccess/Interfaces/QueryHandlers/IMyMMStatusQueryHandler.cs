namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IMyMMStatusQueryHandler
{
    Task<List<MyMMStatusModel>> GetMyMMStatusesAsync();

    Task<MyMMStatusModel> GetMyMMStatusAsync(int myMMStatusId);
}
