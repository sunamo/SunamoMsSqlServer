namespace SunamoMsSqlServer.Services;

using SunamoMsSqlServer._sunamo;

/// <summary>
/// Service for managing unique ID generation and identity insert permissions on MS SQL Server tables.
/// </summary>
/// <param name="logger">The logger instance for error reporting.</param>
/// <param name="dbContext">The Entity Framework DbContext for database access.</param>
public class UniqueIdService(ILogger logger, DbContext dbContext)
{
    /// <summary>
    /// Revokes INSERT permission on the specified table.
    /// </summary>
    /// <param name="tableName">The name of the table to revoke INSERT permission from.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task RevokeInsert(string tableName)
    {
        // TODO: Need to wait until the database connection is released. Otherwise, inserting with a duplicate ID could occur.
        await RunSqlCommand(tableName, true);
    }

    /// <summary>
    /// Grants INSERT permission on the specified table.
    /// </summary>
    /// <param name="tableName">The name of the table to grant INSERT permission on.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task GrantInsert(string tableName)
    {
        await RunSqlCommand(tableName, false);
    }

    private Task RunSqlCommand(string tableName, bool isRevoking)
    {
        ThrowEx.NotImplementedMethod();
        return Task.CompletedTask;
    }

    /// <summary>
    /// Returns the next unique integer ID for the specified table column by finding the current maximum and incrementing it.
    /// </summary>
    /// <param name="tableName">The name of the table to query.</param>
    /// <param name="columnName">The name of the column to find the maximum value in.</param>
    /// <returns>A result containing the next unique integer ID or an exception message.</returns>
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
            SqlCommand command = new SqlCommand(sqlCommandText, sqlConnection);
            var scalar = await command.ExecuteScalarAsync();
            if (scalar == null)
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
