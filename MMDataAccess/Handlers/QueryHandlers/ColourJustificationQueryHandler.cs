namespace MMDataAccess.Handlers.QueryHandlers;

public class ColourJustificationQueryHandler(ManufacturerManagerContext context) : IColourJustificationQueryHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task<ColourJustificationModel> GetColourJustificationAsync(int colourJustificationId) =>
        await _context.ColourJustifications
            .Include(c => c.Widgets)
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.ColourJustificationId == colourJustificationId)
            ?? throw new ArgumentNullException(nameof(colourJustificationId), "Colour Justification not found");

    public async Task<List<ColourJustificationModel>> GetColourJustificationsAsync() =>
        await _context.ColourJustifications
            .Include(c => c.Widgets)
            .AsNoTracking()
            .OrderBy(c => c.Justification)
            .ToListAsync();
}
