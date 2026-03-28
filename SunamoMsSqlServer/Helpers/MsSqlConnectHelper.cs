namespace SunamoMsSqlServer.Helpers;

/// <summary>
/// Provides helper methods for opening and closing SQL Server connections.
/// </summary>
public class MsSqlConnectHelper
{
    /// <summary>
    /// Opens the SQL connection if it is not already open.
    /// </summary>
    /// <param name="connection">The SQL connection to open.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task Open(SqlConnection connection)
    {
        if (connection.State != ConnectionState.Open)
        {
            await connection.OpenAsync();
        }
    }

    /// <summary>
    /// Closes the SQL connection if it is not already closed.
    /// </summary>
    /// <param name="connection">The SQL connection to close.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task Close(SqlConnection connection)
    {
        if (connection.State != ConnectionState.Closed)
        {
            await connection.CloseAsync();
        }
    }
}
