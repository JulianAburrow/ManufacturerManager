namespace MMDataAccess.Interfaces.CommandHandlers;

public interface IMyMMCommandHandler
{
    Task CreateMyMMAsync(MyMMModel myMM);

    Task UpdateMyMMAsync(MyMMModel myMM);

    Task DeleteMyMMAsync(int myMMid);
}
