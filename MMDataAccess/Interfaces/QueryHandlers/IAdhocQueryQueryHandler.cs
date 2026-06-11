namespace MMDataAccess.Interfaces.QueryHandlers;

public interface IAdhocQueryQueryHandler
{
    Task<List<AdhocQueryModel>> GetAdhocQueriesAsync();

    Task<AdhocQueryModel> GetAdhocQueryAsync(int adhocQueryId);

    Task<List<AdhocQueryListModel>> GetLastXSuccessfulAdhocQueries(int numberOfQueriesRequired);
}
