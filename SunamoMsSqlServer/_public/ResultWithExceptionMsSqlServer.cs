namespace SunamoMsSqlServer._public;
public class ResultWithExceptionMsSqlServer<T>
{
    public T? Data { get; set; }
    /// <summary>
    ///     only string, because Message property isn't editable after instatiate
    /// </summary>
    public string? Exc { get; set; }
    public ResultWithExceptionMsSqlServer(T data)
    {
        Data = data;
    }
    public ResultWithExceptionMsSqlServer(string exc)
    {
        Exc = exc;
    }
    public ResultWithExceptionMsSqlServer(Exception exc)
    {
        Exc = exc.Message;
    }
    /// <summary>
    /// In case the data is a string which is also an exception type
    /// </summary>
    public ResultWithExceptionMsSqlServer()
    {
    }
}