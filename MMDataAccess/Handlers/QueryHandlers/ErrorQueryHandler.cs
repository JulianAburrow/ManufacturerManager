namespace MMDataAccess.Handlers.QueryHandlers;

public class ErrorQueryHandler(ManufacturerManagerContext context) : IErrorQueryHandler
{
    public async Task<ErrorModel> GetErrorAsync(int errorId) =>
        await context.Errors
            .AsNoTracking()
        .SingleOrDefaultAsync(e => e.ErrorId == errorId)
        ?? throw new ArgumentNullException(nameof(errorId), "Error not found");

    public async Task<List<ErrorModel>> GetErrorsAsync() =>
        await context.Errors
        .AsNoTracking()
        .ToListAsync();
}
