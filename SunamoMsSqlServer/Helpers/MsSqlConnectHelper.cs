namespace SunamoMsSqlServer.Helpers;

public class MsSqlConnectHelper
{
    public static async Task Open(SqlConnection connection)
    {
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }
    }

    public static async Task Close(SqlConnection connection)
    {
        if (connection.State != ConnectionState.Closed)
        {
            await connection.CloseAsync();
        }
    }
}
