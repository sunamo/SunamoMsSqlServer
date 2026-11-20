namespace SunamoMsSqlServer.Services;

/// <summary>
/// Ms sql service.
/// </summary>
/// <param name="dbContext">The db context.</param>
/// <param name="logger">The logger.</param>
/// <returns>The ms sql service result.</returns>
public class MsSqlService(DbContext dbContext, ILogger logger)
{
    /// <summary>
    /// Get and open connection.
    /// </summary>
    /// <returns>The get and open connection result.</returns>
    public async Task<ResultWithExceptionMsSqlServer<SqlConnection>> GetAndOpenConnection()
    {
        var databaseConnection = dbContext.Database.GetDbConnection();
        var connection = databaseConnection as SqlConnection;
        if (connection is null)
        {
            var exceptionMessage = $"SqlConnection is default, dbConn is {databaseConnection}";
            logger.LogError(exceptionMessage);
            return new ResultWithExceptionMsSqlServer<SqlConnection>(exceptionMessage);
        }
        await MsSqlConnectHelper.Open(connection);
        return new ResultWithExceptionMsSqlServer<SqlConnection>(connection);
    }

    /// <summary>
    /// Delete all.
    /// </summary>
    /// <param name="tableName">The table name.</param>
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
