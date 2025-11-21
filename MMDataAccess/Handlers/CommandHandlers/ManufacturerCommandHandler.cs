namespace MMDataAccess.Handlers.CommandHandlers;

public class ManufacturerCommandHandler(ManufacturerManagerContext context) : IManufacturerCommandHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task CreateManufacturerAsync(ManufacturerModel manufacturer, bool callSaveChanges)
    {
        _context.Manufacturers.Add(manufacturer);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteManufacturerAsync(int manufacturerId, bool callSaveChanges)
    {
        var manufacturerToDelete = _context.Manufacturers.SingleOrDefault(m => m.ManufacturerId == manufacturerId);
        if (manufacturerToDelete is null)
            return;
        _context.Manufacturers.Remove(manufacturerToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task UpdateManufacturerAsync(ManufacturerModel manufacturer, bool callSaveChanges)
    {
        var manufacturerToUpdate = _context.Manufacturers.SingleOrDefault(m => m.ManufacturerId == manufacturer.ManufacturerId);
        if (manufacturerToUpdate is null)
            return;
        manufacturerToUpdate.Name = manufacturer.Name;
        manufacturerToUpdate.StatusId = manufacturer.StatusId;

        if (manufacturer.StatusId == (int)PublicEnums.ManufacturerStatusEnum.Inactive)
        {
            var widgets = _context.Widgets
                .Where(w => w.ManufacturerId == manufacturer.ManufacturerId);
            foreach (var widget in widgets)
            {
                widget.StatusId = (int)PublicEnums.WidgetStatusEnum.Inactive;
            }
        }

        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await _context.SaveChangesAsync();
}
