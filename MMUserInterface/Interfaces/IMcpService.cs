namespace MMUserInterface.Interfaces;

public interface IMcpService
{
    Task<string> GetSqlStringFromNaturalQuery(string query);
}
