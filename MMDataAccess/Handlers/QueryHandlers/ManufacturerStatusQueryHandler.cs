namespace MMDataAccess.Handlers.QueryHandlers;

public class ManufacturerStatusQueryHandler(ManufacturerManagerContext context) : IManufacturerStatusQueryHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task<List<ManufacturerStatusModel>> GetManufacturerStatusesAsync() =>
        await _context.ManufacturerStatuses
            .AsNoTracking()
            .ToListAsync();
}
