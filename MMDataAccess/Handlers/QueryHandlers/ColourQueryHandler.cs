namespace MMDataAccess.Handlers.QueryHandlers;

public class ColourQueryHandler(ManufacturerManagerContext context) : IColourQueryHandler
{
    public async Task<ColourModel> GetColourAsync(int colourId) =>
        await context.Colours
            .Include(c => c.Widgets)
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.ColourId == colourId)
            ?? throw new ArgumentNullException(nameof(colourId), "Colour not found");

    public async Task<List<ColourModel>> GetColoursAsync() =>
        await context.Colours
            .Include(c => c.Widgets)
            .OrderBy(c => c.Name)
            .AsNoTracking()
            .ToListAsync();
}
