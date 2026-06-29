namespace MMDataAccess.Handlers.QueryHandlers;

public class AdhocQueryQueryHandler(ManufacturerManagerContext context) : IAdhocQueryQueryHandler
{
    public async Task<List<AdhocQueryModel>> GetAdhocQueriesAsync() =>
        await context.AdhocQueries
        .OrderByDescending(a => a.WhenRun)
        .AsNoTracking()
        .ToListAsync();

    public async Task<AdhocQueryModel> GetAdhocQueryAsync(int adhocQueryId) =>
        await context.AdhocQueries
        .AsNoTracking()
        .SingleOrDefaultAsync(a => a.AdhocQueryId == adhocQueryId)
        ?? throw new ArgumentNullException(nameof(adhocQueryId), "Ad hoc query not found.");

    public async Task<List<AdhocQueryListModel>> GetLastXSuccessfulAdhocQueries(int numberOfQueriesRequired)
    {
        return await context.AdhocQueries
            .Where(a => a.IsSuccessful)
            .GroupBy(a => a.NaturalLanguageQuery)
            .Select(g => new
            {
                NaturalLanguageQuery = g.Key,
                LatestWhenRun = g.Max(x => x.WhenRun)
            })
            .OrderByDescending(x => x.LatestWhenRun)
            .Take(numberOfQueriesRequired)
            .Select(x => new AdhocQueryListModel
            {
                NaturalLanguageQuery = x.NaturalLanguageQuery
            })
            .ToListAsync();
    }
}
