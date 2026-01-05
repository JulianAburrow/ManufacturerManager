namespace MMDataAccess.Handlers.QueryHandlers;

public class ManufacturerStatusQueryHandler(ManufacturerManagerContext context) : IManufacturerStatusQueryHandler
{
    public async Task<List<ManufacturerStatusModel>> GetManufacturerStatusesAsync() =>
        await context.ManufacturerStatuses
            .AsNoTracking()
            .ToListAsync();
}
