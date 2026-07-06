namespace MMDataAccess.Handlers.CommandHandlers;

public class AdhocQueryCommandHandler(ManufacturerManagerContext context) : IAdhocQueryCommandHandler
{
    public async Task CreateAdhocQueryAsync(AdhocQueryModel adhocQueryModel)
    {
        context.AdhocQueries.Add(adhocQueryModel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAdhocQueryAsync(int adhocQueryId)
    {
        var adhocQueryToDelete = context.AdhocQueries.SingleOrDefault(a => a.AdhocQueryId == adhocQueryId);
        if (adhocQueryToDelete is null)
            return;
        context.AdhocQueries.Remove(adhocQueryToDelete);
        await context.SaveChangesAsync();
    }
}
