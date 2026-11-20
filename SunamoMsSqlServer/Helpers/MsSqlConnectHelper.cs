namespace SunamoMsSqlServer.Helpers;

/// <summary>
/// Represents the ms sql connect helper type.
/// </summary>
public class MsSqlConnectHelper
{
    /// <summary>
    /// Open.
    /// </summary>
    /// <param name="connection">The connection.</param>
    public static async Task Open(SqlConnection connection)
    {
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }
    }

    /// <summary>
    /// Close.
    /// </summary>
    /// <param name="connection">The connection.</param>
    public static async Task Close(SqlConnection connection)
    {
        if (connection.State != ConnectionState.Closed)
        {
            await connection.CloseAsync();
        }
    }
}
