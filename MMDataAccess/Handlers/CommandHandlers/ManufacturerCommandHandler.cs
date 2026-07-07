namespace MMDataAccess.Handlers.CommandHandlers;

public class ManufacturerCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IManufacturerCommandHandler
{
    public async Task CreateManufacturerAsync(ManufacturerModel manufacturer)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        context.Manufacturers.Add(manufacturer);
        await context.SaveChangesAsync();
    }

    public async Task DeleteManufacturerAsync(int manufacturerId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var manufacturerToDelete = await context.Manufacturers.SingleOrDefaultAsync(m => m.ManufacturerId == manufacturerId);
        if (manufacturerToDelete is null)
            return;
        context.Manufacturers.Remove(manufacturerToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateManufacturerAsync(ManufacturerModel manufacturer)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();

        var manufacturerToUpdate = await context.Manufacturers
            .SingleOrDefaultAsync(m => m.ManufacturerId == manufacturer.ManufacturerId);

        if (manufacturerToUpdate is null)
            return;

        manufacturerToUpdate.Name = manufacturer.Name;
        manufacturerToUpdate.StatusId = manufacturer.StatusId;

        if (manufacturer.StatusId == (int)PublicEnums.ManufacturerStatusEnum.Inactive)
        {
            var widgets = await context.Widgets
                .Where(w => w.ManufacturerId == manufacturer.ManufacturerId)
                .ToListAsync();

            foreach (var widget in widgets)
            {
                widget.StatusId = (int)PublicEnums.WidgetStatusEnum.Inactive;
            }
        }

        await context.SaveChangesAsync();
    }
}