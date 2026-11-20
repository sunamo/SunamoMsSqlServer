namespace SunamoMsSqlServer._public;

/// <summary>
/// Represents the result with exception ms sql server t type.
/// </summary>
public class ResultWithExceptionMsSqlServer<T>
{
    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets the exc.
    /// </summary>
    public string? Exc { get; set; }

    /// <summary>
    /// Result with exception ms sql server.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>The result with exception ms sql server result.</returns>
    public ResultWithExceptionMsSqlServer(T data)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the result with exception ms sql server class.
    /// </summary>
    /// <param name="data">The data.</param>
    public ResultWithExceptionMsSqlServer(string exceptionMessage)
    {
        Exc = exceptionMessage;
    }

    /// <summary>
    /// Initializes a new instance of the result with exception ms sql server class.
    /// </summary>
    /// <param name="exceptionMessage">The exception message.</param>
    public ResultWithExceptionMsSqlServer(Exception exception)
    {
        Exc = exception.Message;
    }

    /// <summary>
    /// Initializes a new instance of the result with exception ms sql server class.
    /// </summary>
    /// <param name="exception">The exception.</param>
    public ResultWithExceptionMsSqlServer()
    {
    }
}
