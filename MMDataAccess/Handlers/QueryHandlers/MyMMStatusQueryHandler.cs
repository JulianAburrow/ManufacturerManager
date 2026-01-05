namespace MMDataAccess.Handlers.QueryHandlers;

public class MyMMStatusQueryHandler(ManufacturerManagerContext context) : IMyMMStatusQueryHandler
{
    public async Task<List<MyMMStatusModel>> GetMMStatusesAsync() =>
        await context.MyMMStatuses
            .AsNoTracking()
            .ToListAsync();

    public async Task<MyMMStatusModel> GetMyMMStatusAsync(int myMMStatusId) =>
        await context.MyMMStatuses
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.StatusId == myMMStatusId)
            ?? throw new ArgumentNullException(nameof(myMMStatusId), "MyMMStatus not found");
}
