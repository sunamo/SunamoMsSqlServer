namespace SunamoMsSqlServer.Services;

/// <summary>
/// Core service for managing MS SQL Server connections and executing database operations.
/// </summary>
/// <param name="dbContext">The Entity Framework DbContext for database access.</param>
/// <param name="logger">The logger instance for error reporting.</param>
public class MsSqlService(DbContext dbContext, ILogger logger)
{
    /// <summary>
    /// Retrieves and opens the underlying SQL Server connection from the DbContext.
    /// </summary>
    /// <returns>A result containing the opened SqlConnection or an exception message.</returns>
    public async Task<ResultWithExceptionMsSqlServer<SqlConnection>> GetAndOpenConnection()
    {
        var databaseConnection = dbContext.Database.GetDbConnection();
        var connection = databaseConnection as SqlConnection;
        if (connection == null)
        {
            var exceptionMessage = $"SqlConnection is default, dbConn is {databaseConnection}";
            logger.LogError(exceptionMessage);
            return new ResultWithExceptionMsSqlServer<SqlConnection>(exceptionMessage);
        }
        await MsSqlConnectHelper.Open(connection);
        return new ResultWithExceptionMsSqlServer<SqlConnection>(connection);
    }

    /// <summary>
    /// Deletes all rows from the specified table.
    /// </summary>
    /// <param name="tableName">The name of the table to delete all rows from.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeleteAll(string tableName)
    {
        var connectionResult = await GetAndOpenConnection();
        if (connectionResult.Data == default)
        {
            return;
        }
        var connection = connectionResult.Data;
        SqlCommand command = new($"delete from {tableName}", connectionResult.Data);
        await command.ExecuteNonQueryAsync();
        await MsSqlConnectHelper.Close(connection);
    }
}
