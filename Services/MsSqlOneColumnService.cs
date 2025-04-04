namespace SunamoMsSqlServer.Services;

public class MsSqlOneColumnService(MsSqlService msSqlService)
{
    public async Task<ResultWithExceptionMsSqlServer<List<int>>> Int(string tableName, string column)
    {
        List<int> l = [];
        SqlCommand cmd = new($"select {column} from {tableName}");
        var connResult = await msSqlService.GetAndOpenConnection();
        if (connResult.Exc != null)
        {
            return new ResultWithExceptionMsSqlServer<List<int>>(connResult.Exc);
        }
        cmd.Connection = connResult.Data;
        var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            l.Add(reader.GetInt32(0));
        }
        return new ResultWithExceptionMsSqlServer<List<int>>(l);
    }
}