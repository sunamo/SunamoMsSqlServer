namespace SunamoMsSqlServer.Services;

public class MsSqlService(DbContext dbContext, ILogger logger)
{
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
