namespace MMDataAccess.Handlers.CommandHandlers;

public class ErrorCommandHandler(IDbContextFactory<ManufacturerManagerContext> manufacturerManagerContextFactory) : IErrorCommandHandler
{
    public async Task CreateErrorAsync(Exception ex)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var errorModel = new ErrorModel
        {
            ErrorDate = DateTime.Now,
            ErrorMessage = ex.Message,
            Exception = ex.GetType().ToString(),
            InnerException = ex.InnerException?.Message ?? "No inner exception",
            StackTrace = ex.StackTrace,
        };
        context.Errors.Add(errorModel);
        await context.SaveChangesAsync();
    }

    public async Task DeleteErrorAsync(int errorId)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var errorToDelete = await context.Errors.SingleOrDefaultAsync(e => e.ErrorId == errorId);
        if (errorToDelete is null)
            return;
        context.Errors.Remove(errorToDelete);
        await context.SaveChangesAsync();
    }

    public async Task UpdateErrorAsync(ErrorModel error)
    {
        await using var context = await manufacturerManagerContextFactory.CreateDbContextAsync();
        var errorToUpdate = await context.Errors.SingleOrDefaultAsync(e => e.ErrorId == error.ErrorId);
        if (errorToUpdate is null)
            return;

        errorToUpdate.Resolved = error.Resolved;
        errorToUpdate.ResolvedDate = error.ResolvedDate;
        await context.SaveChangesAsync();
    }
}
