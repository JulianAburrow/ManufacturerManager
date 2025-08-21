namespace MMDataAccess.Handlers.QueryHandlers;

public class WidgetQueryHandler(ManufacturerManagerContext context) : IWidgetQueryHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task<WidgetModel> GetWidgetAsync(int widgetId) =>
        await _context.Widgets
            .Include(w => w.Colour)
            .Include(w => w.ColourJustification)
            .Include(w => w.Manufacturer)
            .Include(w => w.Status)
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.WidgetId == widgetId)
            ?? throw new ArgumentNullException(nameof(widgetId), "Widget not found");

    public async Task<List<WidgetModel>> GetWidgetsAsync() =>
        await _context.Widgets
        .Include(w => w.Manufacturer)
        .Include(w => w.Colour)
        .Include(w => w.Status)
        .OrderBy(w => w.Name)
        .AsNoTracking()
        .ToListAsync();
}
