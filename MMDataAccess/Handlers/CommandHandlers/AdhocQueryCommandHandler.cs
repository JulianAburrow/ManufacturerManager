namespace MMDataAccess.Handlers.CommandHandlers;

public class AdhocQueryCommandHandler(ManufacturerManagerContext context) : IAdhocQueryCommandHandler
{
    public async Task CreateAdhocQueryAsync(AdhocQueryModel adhocQueryModel, bool callSaveChanges)
    {
        context.AdhocQueries.Add(adhocQueryModel);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteAdhocQueryAsync(int adhocQueryId, bool callSaveChanges)
    {
        var adhocQueryToDelete = context.AdhocQueries.SingleOrDefault(a => a.AdhocQueryId == adhocQueryId);
        if (adhocQueryToDelete is null)
            return;
        context.AdhocQueries.Remove(adhocQueryToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
