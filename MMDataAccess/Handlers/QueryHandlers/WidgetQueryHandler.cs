namespace MMDataAccess.Handlers.QueryHandlers;

public class WidgetQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IWidgetQueryHandler
{
    public async Task<WidgetModel> GetWidgetAsync(int widgetId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var widget = await context.Widgets
            .Include(w => w.Colour)
            .Include(w => w.ColourJustification)
            .Include(w => w.Manufacturer)
            .Include(w => w.Status)
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.WidgetId == widgetId);

        return widget ?? new WidgetModel();
    }

    public async Task<List<WidgetSummary>> GetWidgetsAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Widgets
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
}
