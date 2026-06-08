namespace MMDataAccess.MCP;

public class McpSqlExecutor
{
    private readonly string _connectionString;

    public McpSqlExecutor(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("McpSql")
            ?? throw new InvalidOperationException("Connection string 'McpSql' not found.");
    }

    public async Task<DataTable> ExecuteQueryAsync(string sql)
    {
        if (string.IsNullOrWhiteSpace(sql))
            throw new ArgumentException("SQL query cannot be null or empty.", nameof(sql));

        var dataTable = new DataTable();

        await using var connection = new SqlConnection(_connectionString);
        await using var command = new SqlCommand(sql, connection);

        command.CommandType = CommandType.Text;
        command.CommandTimeout = 30; // sensible default

        await connection.OpenAsync();

        await using var reader = await command.ExecuteReaderAsync();
        dataTable.Load(reader);

        return dataTable;
    }
}