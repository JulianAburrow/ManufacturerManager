namespace MMDataAccess.Handlers.QueryHandlers;

public class ManufacturerStatusQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IManufacturerStatusQueryHandler
{
    public async Task<List<ManufacturerStatusModel>> GetManufacturerStatusesAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.ManufacturerStatuses
            .AsNoTracking()
            .ToListAsync();
    }
}
