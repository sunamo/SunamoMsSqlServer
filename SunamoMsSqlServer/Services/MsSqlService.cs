// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoMsSqlServer.Services;

public class MsSqlService(DbContext db, ILogger logger)
{
    public async Task<ResultWithExceptionMsSqlServer<SqlConnection>> GetAndOpenConnection()
    {
        var dbConn = db.Database.GetDbConnection();
        var conn = dbConn as SqlConnection;
        if (conn == null)
        {
            var exc = $"SqlConnection is default, dbConn is {dbConn}";
            logger.LogError(exc);
            return new ResultWithExceptionMsSqlServer<SqlConnection>(exc);
        }
        await MsSqlConnectHelper.Open(conn);
        return new ResultWithExceptionMsSqlServer<SqlConnection>(conn);
    }
    public async Task DeleteAll(string tableName)
    {
        var connResult = await GetAndOpenConnection();
        if (connResult.Data == default)
        {
            return;
        }
        var conn = connResult.Data;
        SqlCommand comm = new($"delete from {tableName}", connResult.Data);
        await comm.ExecuteNonQueryAsync();
        await MsSqlConnectHelper.Close(conn);
    }
}