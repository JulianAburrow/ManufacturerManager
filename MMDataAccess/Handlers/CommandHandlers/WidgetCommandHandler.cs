namespace MMDataAccess.Handlers.CommandHandlers;

public class WidgetCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IWidgetCommandHandler
{    
    public async Task CreateWidgetAsync(WidgetModel widget)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.Widgets.Add(widget);
        await context.SaveChangesAsync();
    }

    public async Task DeleteWidgetAsync(int widgetId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var widgetToDelete = await context.Widgets.SingleOrDefaultAsync(w => w.WidgetId == widgetId);
        if (widgetToDelete is null)
            return;
        context.Widgets.Remove(widgetToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateWidgetAsync(WidgetModel widget)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var widgetToUpdate = await context.Widgets.SingleOrDefaultAsync(w => w.WidgetId == widget.WidgetId);
        if (widgetToUpdate is null)
            return;
        widgetToUpdate.Name = widget.Name;
        widgetToUpdate.ColourId = widget.ColourId;
        widgetToUpdate.ColourJustificationId = widget.ColourJustificationId;
        widgetToUpdate.ManufacturerId = widget.ManufacturerId;
        widgetToUpdate.StatusId = widget.StatusId;
        widgetToUpdate.CostPrice = widget.CostPrice;
        widgetToUpdate.RetailPrice = widget.RetailPrice;
        widgetToUpdate.StockLevel = widget.StockLevel;

        await context.SaveChangesAsync();
    }
}
