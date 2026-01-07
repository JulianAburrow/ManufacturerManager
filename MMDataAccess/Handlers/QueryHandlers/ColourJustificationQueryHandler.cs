namespace MMDataAccess.Handlers.QueryHandlers;

public class ColourJustificationQueryHandler(ManufacturerManagerContext context) : IColourJustificationQueryHandler
{
    public async Task<ColourJustificationModel> GetColourJustificationAsync(int colourJustificationId) =>
        await context.ColourJustifications
            .Include(c => c.Widgets)
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.ColourJustificationId == colourJustificationId)
            ?? throw new ArgumentNullException(nameof(colourJustificationId), "Colour Justification not found");

    public async Task<List<ColourJustificationModel>> GetColourJustificationsAsync() =>
        await context.ColourJustifications
            .Include(c => c.Widgets)
            .AsNoTracking()
            .OrderBy(c => c.Justification)
            .ToListAsync();
}
