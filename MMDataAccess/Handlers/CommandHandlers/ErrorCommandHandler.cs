namespace MMDataAccess.Handlers.CommandHandlers;

public class ErrorCommandHandler(ManufacturerManagerContext context) : IErrorCommandHandler
{
    public async Task CreateErrorAsync(Exception ex, bool callSaveChanges)
    {
        var errorModel = new ErrorModel
        {
            ErrorDate = DateTime.Now,
            ErrorMessage = ex.Message,
            Exception = ex.GetType().ToString(),
            InnerException = ex.InnerException?.Message ?? "No inner exception",
            StackTrace = ex.StackTrace,
        };
        context.Errors.Add(errorModel);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task DeleteErrorAsync(int errorId, bool callSaveChanges)
    {
        var errorToDelete = context.Errors.SingleOrDefault(e => e.ErrorId == errorId);
        if (errorToDelete is null)
            return;
        context.Errors.Remove(errorToDelete);
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task UpdateErrorAsync(ErrorModel error, bool callSaveChanges)
    {
        var errorToUpdate = context.Errors.SingleOrDefault(e => e.ErrorId == error.ErrorId);
        if (errorToUpdate is null)
            return;

        errorToUpdate.Resolved = error.Resolved;
        errorToUpdate.ResolvedDate = error.ResolvedDate;
        if (callSaveChanges)
            await SaveChangesAsync();
    }

    public async Task SaveChangesAsync() =>
        await context.SaveChangesAsync();
}
