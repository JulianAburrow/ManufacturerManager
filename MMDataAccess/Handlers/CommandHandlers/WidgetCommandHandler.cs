namespace MMDataAccess.Handlers.CommandHandlers;

public class WidgetCommandHandler(ManufacturerManagerContext context) : IWidgetCommandHandler
{    
    public async Task CreateWidgetAsync(WidgetModel widget, bool callSaveChanges)
    {
        context.Widgets.Add(widget);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteWidgetAsync(int widgetId, bool callSaveChanges)
    {
        var widgetToDelete = context.Widgets.SingleOrDefault(w => w.WidgetId == widgetId);
        if (widgetToDelete is null)
            return;
        context.Widgets.Remove(widgetToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task UpdateWidgetAsync(WidgetModel widget, bool callSaveChanges)
    {
        var widgetToUpdate = context.Widgets.SingleOrDefault(w => w.WidgetId == widget.WidgetId);
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
        widgetToUpdate.WidgetImage = widget.WidgetImage;

        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
