namespace MMDataAccess.Handlers.CommandHandlers;

public class ManufacturerCommandHandler(ManufacturerManagerContext context) : IManufacturerCommandHandler
{
    public async Task CreateManufacturerAsync(ManufacturerModel manufacturer)
    {
        context.Manufacturers.Add(manufacturer);
        await SaveChangesAsync();
    }

    public async Task DeleteManufacturerAsync(int manufacturerId)
    {
        var manufacturerToDelete = context.Manufacturers.SingleOrDefault(m => m.ManufacturerId == manufacturerId);
        if (manufacturerToDelete is null)
            return;
        context.Manufacturers.Remove(manufacturerToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateManufacturerAsync(ManufacturerModel manufacturer)
    {
        using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var manufacturerToUpdate = context.Manufacturers.SingleOrDefault(m => m.ManufacturerId == manufacturer.ManufacturerId);
        if (manufacturerToUpdate is null)
            return;

        manufacturerToUpdate.Name = manufacturer.Name;
        manufacturerToUpdate.StatusId = manufacturer.StatusId;

        if (manufacturer.StatusId == (int)PublicEnums.ManufacturerStatusEnum.Inactive)
        {
            var widgets = context.Widgets
                .Where(w => w.ManufacturerId == manufacturer.ManufacturerId);
            foreach (var widget in widgets)
            {
                widget.StatusId = (int)PublicEnums.WidgetStatusEnum.Inactive;
            }
        }

        await context.SaveChangesAsync();

        scope.Complete();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}