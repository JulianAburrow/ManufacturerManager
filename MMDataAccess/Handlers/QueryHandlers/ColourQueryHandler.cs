namespace MMDataAccess.Handlers.QueryHandlers;

public class ColourQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IColourQueryHandler
{
    public async Task<ColourModel> GetColourAsync(int colourId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Colours
            .Include(c => c.Widgets)
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.ColourId == colourId)
            ?? throw new KeyNotFoundException($"Colour with ID {colourId} not found.");
    }

    public async Task<List<ColourModel>> GetColoursAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Colours
            .Include(c => c.Widgets)
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync();
    }
}