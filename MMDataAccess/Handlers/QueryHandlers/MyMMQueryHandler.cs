namespace MMDataAccess.Handlers.QueryHandlers;

public class MyMMQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IMyMMQueryHandler
{
    public async Task<MyMMModel> GetMyMMAsync(int myMMId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var myMM = await context.MyMMs
            .Include(m => m.Status)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.MyMMId == myMMId);
        return myMM ?? new MyMMModel();
    }
        

    public async Task<List<MyMMModel>> GetMyMMsAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.MyMMs
            .Include(m => m.Status)
            .OrderBy(m => m.ActionDate)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<MyMMModel>> GetMyMMsForHomePageAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.MyMMs
            .Include(m => m.Status)
            .OrderBy(m => m.ActionDate)
            .AsNoTracking()
            .Where(m =>
                m.ActionDate < DateTime.Today.AddDays(1) &&
                (m.StatusId == (int)PublicEnums.MyMMStatusEnum.Active ||
                m.StatusId == (int)PublicEnums.MyMMStatusEnum.Pending))
            .ToListAsync();
    }
}
