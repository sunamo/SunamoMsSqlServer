// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoMsSqlServer.Helpers;

public class MsSqlConnectHelper
{
    public static async Task Open(SqlConnection conn)
    {
        if (conn.State != ConnectionState.Open)
        {
            await conn.OpenAsync();
        }
    }
    public static async Task Close(SqlConnection conn)
    {
        if (conn.State != ConnectionState.Closed)
        {
            await conn.CloseAsync();
        }
    }
}