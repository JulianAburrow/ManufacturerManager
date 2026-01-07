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
            .OrderBy(m => m.ActionDate)
            .AsNoTracking()
            .ToListAsync();

    public async Task<List<MyMMModel>> GetMyMMsForHomePageAsync() =>
        await context.MyMMs
            .Include(m => m.Status)
            .OrderBy(m => m.ActionDate)
            .AsNoTracking()
            .Where(m =>
                m.ActionDate < DateTime.Today.AddDays(1) &&
                (m.StatusId == (int)PublicEnums.MyMMStatusEnum.Active ||
                m.StatusId == (int)PublicEnums.MyMMStatusEnum.Pending))
            .ToListAsync();
}
