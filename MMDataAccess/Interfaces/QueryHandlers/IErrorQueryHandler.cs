namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IErrorQueryHandler
{
    Task<ErrorModel> GetErrorAsync(int errorId);

    Task<List<ErrorModel>> GetErrorsAsync();
}
