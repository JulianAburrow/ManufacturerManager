namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IAdhocQueryCommandHandler
{
    Task CreateAdhocQueryAsync(AdhocQueryModel adhocQueryModel);

    Task DeleteAdhocQueryAsync(int adhocQueryId);
}
