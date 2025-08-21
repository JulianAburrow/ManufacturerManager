namespace MMDataAccess.Handlers.QueryHandlers;

public class WidgetStatusQueryHandler(ManufacturerManagerContext context) : IWidgetStatusQueryHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task<List<WidgetStatusModel>> GetWidgetStatusesAsync() =>
        await _context.WidgetStatuses
            .AsNoTracking()
            .ToListAsync();
}
