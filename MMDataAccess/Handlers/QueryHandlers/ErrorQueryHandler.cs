namespace MMDataAccess.Handlers.QueryHandlers;

public class ErrorQueryHandler(ManufacturerManagerContext context) : IErrorQueryHandler
{
    private readonly ManufacturerManagerContext _context = context;

    public async Task<ErrorModel> GetErrorAsync(int errorId) =>
        await _context.Errors
            .AsNoTracking()
        .SingleOrDefaultAsync(e => e.ErrorId == errorId)
        ?? throw new ArgumentNullException(nameof(errorId), "Error not found");

    public async Task<List<ErrorModel>> GetErrorsAsync() =>
        await _context.Errors
        .AsNoTracking()
        .ToListAsync();
}
