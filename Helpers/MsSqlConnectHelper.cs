namespace SunamoMsSqlServer.Helpers;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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