namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IErrorCommandHandler
{
    Task CreateErrorAsync(Exception ex);

    Task UpdateErrorAsync(ErrorModel error);

    Task DeleteErrorAsync(int errorId);
}
