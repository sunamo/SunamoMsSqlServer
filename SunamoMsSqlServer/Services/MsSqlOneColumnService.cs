namespace SunamoMsSqlServer.Services;

public class MsSqlOneColumnService(MsSqlService msSqlService)
{
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
