namespace MMDataAccess.Handlers.QueryHandlers;

public class MyMMQueryHandler(ManufacturerManagerContext context) : IMyMMQueryHandler
{
    public async Task<MyMMModel> GetMyMMAsync(int myMMId) =>
        await context.MyMMs
        .Include(m => m.Status)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.MyMMId == myMMId)
            ?? throw new ArgumentNullException(nameof(myMMId), "MyMM not found");

    public async Task<List<MyMMModel>> GetMyMMsAsync() =>
        await context.MyMMs
            .Include(m => m.Status)
            .AsNoTracking()
            .ToListAsync();
}
