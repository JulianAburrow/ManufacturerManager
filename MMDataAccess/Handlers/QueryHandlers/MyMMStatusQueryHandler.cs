namespace MMDataAccess.Handlers.QueryHandlers;

public class MyMMStatusQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IMyMMStatusQueryHandler
{
    public async Task<List<MyMMStatusModel>> GetMyMMStatusesAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.MyMMStatuses
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<MyMMStatusModel> GetMyMMStatusAsync(int myMMStatusId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var myMMStatus = await context.MyMMStatuses
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.StatusId == myMMStatusId);

        return myMMStatus ?? new MyMMStatusModel();
    }
}