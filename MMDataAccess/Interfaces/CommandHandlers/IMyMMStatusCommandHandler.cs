namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IMyMMStatusCommandHandler
{
    Task CreateMyMMStatusAsync(MyMMStatusModel myMMStatusModel);

    Task DeleteMyMMStatusAsync(int myMMStatusId);

    Task UpdateMyMMStatusAsync(MyMMStatusModel myMMStatusModel);
}
