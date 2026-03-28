namespace SunamoMsSqlServer._public;

/// <summary>
/// Wraps a result value or an exception message for MS SQL Server operations.
/// </summary>
/// <typeparam name="T">The type of the data result.</typeparam>
public class ResultWithExceptionMsSqlServer<T>
{
    /// <summary>
    /// Gets or sets the data result of the operation.
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Gets or sets the exception message. Stored as string because the Message property is not editable after instantiation.
    /// </summary>
    public string? Exc { get; set; }

    /// <summary>
    /// Initializes a new instance with the specified data result.
    /// </summary>
    /// <param name="data">The data result of the operation.</param>
    public ResultWithExceptionMsSqlServer(T data)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance with the specified exception message.
    /// </summary>
    /// <param name="exceptionMessage">The exception message describing the error.</param>
    public ResultWithExceptionMsSqlServer(string exceptionMessage)
    {
        Exc = exceptionMessage;
    }

    /// <summary>
    /// Initializes a new instance with the message from the specified exception.
    /// </summary>
    /// <param name="exception">The exception whose message will be stored.</param>
    public ResultWithExceptionMsSqlServer(Exception exception)
    {
        Exc = exception.Message;
    }

    /// <summary>
    /// Initializes a new empty instance for cases where data is a string which could also represent an exception.
    /// </summary>
    public ResultWithExceptionMsSqlServer()
    {
    }
}
