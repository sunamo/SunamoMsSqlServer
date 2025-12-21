namespace SunamoMsSqlServer.Services;

using SunamoMsSqlServer._sunamo;

public class UniqueIdService(MsSqlOneColumnService msSqlOneColumn, MsSqlService msSqlService, ILogger logger, DbContext db)
{
    public async Task RevokeInsert(string tableName)
    {
        // TODO: tady musím počkat než se db uvolní. jinak bych mohl vkládat pod stejným ID. Než to bude, musím to zakomentovat v RunSqlCommand
        await RunSqlCommand(tableName, true);
    }
    public async Task GrantInsert(string tableName)
    {
        await RunSqlCommand(tableName, false);
    }
#pragma warning disable
    private async Task RunSqlCommand(string tableName, bool revoke)
#pragma warning enable

    {
        ThrowEx.NotImplementedMethod();

        // čti komentář v RevokeInsert
        return;
        var commandPart = revoke ? "REVOKE" : "GRANT";
        var result = await msSqlService.GetAndOpenConnection();
        var sqlConn = result.Data;
        try
        {
            await MsSqlConnectHelper.Open(sqlConn);
            SqlCommand cmd = new SqlCommand($"{commandPart} INSERT ON {tableName} FROM public;", sqlConn);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
        finally
        {
            await MsSqlConnectHelper.Close(sqlConn);
        }
    }
    public async Task<ResultWithExceptionMsSqlServer<int>> Int(string tableName, string column)
    {
        var dbConn = db.Database.GetDbConnection();
        var sqlConn = dbConn as SqlConnection;
        await MsSqlConnectHelper.Open(sqlConn);
        int max = int.MinValue;
        try
        {
            var sqlCommand = $"SELECT MAX({column}) FROM {tableName};";
            SqlCommand cmd = new SqlCommand(sqlCommand, sqlConn);
            var scalar = await cmd.ExecuteScalarAsync();
            if (scalar == null)
            {
                logger.LogError($"{sqlCommand} return null");
                return new ResultWithExceptionMsSqlServer<int>($"{sqlCommand} return null");
            }
            max = int.Parse(scalar.ToString());
        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
        }
        finally
        {
            await MsSqlConnectHelper.Close(sqlConn);
        }
        //var cells = await msSqlOneColumn.Int(tableName, column);
        //if (cells.Exc != null)
        //{
        //    return new ResultWithExceptionMsSqlServer<int>(cells.Exc);
        //}
        //var max = cells.Data.Max();
        return new(++max);
    }
}