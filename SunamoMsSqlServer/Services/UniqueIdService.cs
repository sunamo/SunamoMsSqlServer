namespace SunamoMsSqlServer.Services;

using SunamoMsSqlServer._sunamo;

public class UniqueIdService(ILogger logger, DbContext dbContext)
{
    public async Task RevokeInsert(string tableName)
    {
        // TODO: Need to wait until the database connection is released. Otherwise, inserting with a duplicate ID could occur.
        await RunSqlCommand(tableName, true);
    }

    public async Task GrantInsert(string tableName)
    {
        await RunSqlCommand(tableName, false);
    }

    private Task RunSqlCommand(string tableName, bool isRevoking)
    {
        ThrowEx.NotImplementedMethod();
        return Task.CompletedTask;
    }

    public async Task<ResultWithExceptionMsSqlServer<int>> Int(string tableName, string columnName)
    {
        var databaseConnection = dbContext.Database.GetDbConnection();
        var sqlConnection = databaseConnection as SqlConnection
            ?? throw new InvalidOperationException($"Database connection is not a SqlConnection, got {databaseConnection.GetType().Name}");
        await MsSqlConnectHelper.Open(sqlConnection);
        int maximumId = int.MinValue;
        try
        {
            var sqlCommandText = $"SELECT MAX({columnName}) FROM {tableName};";
            SqlCommand command = new(sqlCommandText, sqlConnection);
            var scalar = await command.ExecuteScalarAsync();
            if (scalar is null)
            {
                logger.LogError($"{sqlCommandText} return null");
                return new ResultWithExceptionMsSqlServer<int>($"{sqlCommandText} return null");
            }
            maximumId = int.Parse(scalar.ToString()!);
        }
        catch (Exception exception)
        {
            logger.LogError(exception.Message);
        }
        finally
        {
            await MsSqlConnectHelper.Close(sqlConnection);
        }
        return new(++maximumId);
    }
}
