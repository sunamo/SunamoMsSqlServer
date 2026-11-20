namespace SunamoMsSqlServer.Services;

/// <summary>
/// Ms sql one column service.
/// </summary>
/// <param name="msSqlService">The ms sql service.</param>
/// <returns>The ms sql one column service result.</returns>
public class MsSqlOneColumnService(MsSqlService msSqlService)
{
    /// <summary>
    /// Int.
    /// </summary>
    /// <param name="tableName">The table name.</param>
    /// <param name="columnName">The column name.</param>
    /// <returns>The int result.</returns>
    public async Task<ResultWithExceptionMsSqlServer<List<int>>> Int(string tableName, string columnName)
    {
        List<int> list = [];
        SqlCommand command = new($"select {columnName} from {tableName}");
        var connectionResult = await msSqlService.GetAndOpenConnection();
        if (connectionResult.Exc != null)
        {
            return new ResultWithExceptionMsSqlServer<List<int>>(connectionResult.Exc);
        }
        command.Connection = connectionResult.Data;
        var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(reader.GetInt32(0));
        }
        return new ResultWithExceptionMsSqlServer<List<int>>(list);
    }
}
