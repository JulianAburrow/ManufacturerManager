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

    public async Task<List<WidgetSummary>> GetWidgetsAsync() =>
        await _context.Widgets
            .Select(w => new WidgetSummary
            {
                WidgetId = w.WidgetId,
                Name = w.Name,
                ManufacturerName = w.Manufacturer.Name,
                ColourName = w.Colour.Name,
                CostPrice = w.CostPrice,
                StockLevel = w.StockLevel,
                StatusName = w.Status.StatusName,
            })
        .OrderBy(w => w.Name)
        .AsNoTracking()
        .ToListAsync();
}
