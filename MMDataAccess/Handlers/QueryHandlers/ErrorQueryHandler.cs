namespace MMDataAccess.Handlers.QueryHandlers;

public class ErrorQueryHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IErrorQueryHandler
{
    public async Task<ErrorModel> GetErrorAsync(int errorId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Errors
            .AsNoTracking()
        .SingleOrDefaultAsync(e => e.ErrorId == errorId)
        ?? throw new KeyNotFoundException($"Error with ID {errorId} not found.");
    }

    public async Task<List<ErrorModel>> GetErrorsAsync()
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        return await context.Errors
            .AsNoTracking()
            .ToListAsync();
    }
}
