namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IMyMMCommandHandler
{
    Task CreateMyMMAsync(MyMMModel myMM, bool callSaveChanges);

    Task UpdateMyMMAsync(MyMMModel myMM, bool callSaveChanges);

    Task DeleteMyMMAsync(int myMMid, bool callSaveChanges);

    Task SaveChangesAsync();
}
