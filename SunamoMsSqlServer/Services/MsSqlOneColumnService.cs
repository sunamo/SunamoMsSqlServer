namespace SunamoMsSqlServer.Services;

/// <summary>
/// Service for reading single-column data from MS SQL Server tables.
/// </summary>
/// <param name="msSqlService">The underlying MS SQL service for connection management.</param>
public class MsSqlOneColumnService(MsSqlService msSqlService)
{
    /// <summary>
    /// Reads all integer values from a single column of the specified table.
    /// </summary>
    /// <param name="tableName">The name of the database table to read from.</param>
    /// <param name="columnName">The name of the column containing integer values.</param>
    /// <returns>A result containing the list of integers or an exception message.</returns>
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
