namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IErrorCommandHandler
{
    Task CreateErrorAsync(Exception ex, bool callSaveChanges);

    Task UpdateErrorAsync(ErrorModel error, bool callSaveChanges);

    Task DeleteErrorAsync(int errorId, bool callSaveChanges);

    Task SaveChangesAsync();
}
