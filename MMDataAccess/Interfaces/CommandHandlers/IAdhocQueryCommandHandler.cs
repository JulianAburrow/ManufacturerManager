namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IAdhocQueryCommandHandler
{
    Task CreateAdhocQueryAsync(AdhocQueryModel adhocQueryModel, bool callSaveChanges);

    Task DeleteAdhocQueryAsync(int adhocQueryId, bool callSaveChanges);

    Task SaveChangesAsync();
}
