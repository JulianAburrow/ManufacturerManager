namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IMyMMStatusCommandHandler
{
    Task CreateMyMMStatusAsync(MyMMStatusModel myMMStatusModel, bool callSaveChanges);

    Task DeleteMyMMStatusAsync(int myMMStatusId, bool callSaveChanges);

    Task UpdateMyMMStatusAsync(MyMMStatusModel myMMStatusModel, bool callSaveChanges);

    Task SaveChangesAsync();
}
