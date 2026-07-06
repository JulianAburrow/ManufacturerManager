namespace MMDataAccess.Handlers.CommandHandlers;

public class AdhocQueryCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IAdhocQueryCommandHandler
{
    public async Task CreateAdhocQueryAsync(AdhocQueryModel adhocQueryModel)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.AdhocQueries.Add(adhocQueryModel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAdhocQueryAsync(int adhocQueryId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var adhocQueryToDelete = await context.AdhocQueries.SingleOrDefaultAsync(a => a.AdhocQueryId == adhocQueryId);
        if (adhocQueryToDelete is null)
            return;
        context.AdhocQueries.Remove(adhocQueryToDelete);
        await context.SaveChangesAsync();
    }
}
