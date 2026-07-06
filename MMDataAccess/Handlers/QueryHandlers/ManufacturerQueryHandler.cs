namespace MMDataAccess.Handlers.QueryHandlers;

public class ManufacturerQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IManufacturerQueryHandler
{
    public async Task<ManufacturerModel> GetManufacturerAsync(int manufacturerId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Manufacturers
            .Include(m => m.Widgets)
                .ThenInclude(w => w.Colour)
            .Include(m => m.Widgets)
                .ThenInclude(w => w.Status)
            .Include(m => m.Status)
            .AsNoTracking()
            .SingleOrDefaultAsync(m => m.ManufacturerId == manufacturerId)
            ?? throw new KeyNotFoundException($"Manufacturer with ID {manufacturerId} not found.");

    }
        
    public async Task<List<ManufacturerSummary>> GetManufacturersAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Manufacturers
            .Select(m => new ManufacturerSummary
            {
                ManufacturerId = m.ManufacturerId,
                Name = m.Name,
                StatusName = m.Status.StatusName,
                WidgetCount = m.Widgets.Count(),
            })
            .OrderBy(m => m.Name)
            .AsNoTracking()
            .ToListAsync();
    }        

    public async Task<int> GetManufacturerStatusByManufacturerId(int manufacturerId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Manufacturers
            .Where(m => m.ManufacturerId == manufacturerId)
            .Select(s => s.StatusId)
            .SingleOrDefaultAsync();
    }        
}
