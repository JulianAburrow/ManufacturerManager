namespace MMDataAccess.Handlers.QueryHandlers;

public class ColourJustificationQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IColourJustificationQueryHandler
{
    public async Task<ColourJustificationModel> GetColourJustificationAsync(int colourJustificationId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var colourJustification = await context.ColourJustifications
            .Include(c => c.Widgets)
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.ColourJustificationId == colourJustificationId);

        return colourJustification ?? new ColourJustificationModel();
    }

    public async Task<List<ColourJustificationModel>> GetColourJustificationsAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.ColourJustifications
            .Include(c => c.Widgets)
            .AsNoTracking()
            .OrderBy(c => c.Justification)
            .ToListAsync();
    }
}
