namespace MMDataAccess.Handlers.QueryHandlers;

public class WidgetStatusQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IWidgetStatusQueryHandler
{
    public async Task<List<WidgetStatusModel>> GetWidgetStatusesAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.WidgetStatuses
            .AsNoTracking()
            .ToListAsync();
    }
}
