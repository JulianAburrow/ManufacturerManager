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
            .OrderByDescending(a => a.WhenRun)
            .Select(a => a.NaturalLanguageQuery)
            .Distinct() // EF translates this fine because it's just a string
            .Take(numberOfQueriesRequired)
            .Select(q => new AdhocQueryListModel
            {
                NaturalLanguageQuery = q 
            })
            .ToListAsync();
    }
}
