namespace SunamoMsSqlServer._public;

public class ResultWithExceptionMsSqlServer<T>
{
    public T? Data { get; set; }

    public string? Exc { get; set; }

    public ResultWithExceptionMsSqlServer(T data)
    {
        Data = data;
    }

    public ResultWithExceptionMsSqlServer(string exceptionMessage)
    {
        Exc = exceptionMessage;
    }

    public ResultWithExceptionMsSqlServer(Exception exception)
    {
        Exc = exception.Message;
    }

    public ResultWithExceptionMsSqlServer()
    {
    }
}
