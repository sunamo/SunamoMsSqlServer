using SunamoMsSqlServer._public;

namespace SunamoMsSqlServer.Tests;

public class UnitTest1
{
    [Fact]
    public void ResultWithData_SetsDataProperty()
    {
        var result = new ResultWithExceptionMsSqlServer<int>(42);

        Assert.Equal(42, result.Data);
        Assert.Null(result.Exc);
    }

    [Fact]
    public void ResultWithExceptionMessage_SetsExcProperty()
    {
        var result = new ResultWithExceptionMsSqlServer<int>("Connection failed");

        Assert.Equal("Connection failed", result.Exc);
        Assert.Equal(default, result.Data);
    }

    [Fact]
    public void ResultWithException_ExtractsMessage()
    {
        var exception = new InvalidOperationException("Database timeout");
        var result = new ResultWithExceptionMsSqlServer<string>(exception);

        Assert.Equal("Database timeout", result.Exc);
        Assert.Null(result.Data);
    }

    [Fact]
    public void ResultWithDefaultConstructor_HasNullProperties()
    {
        var result = new ResultWithExceptionMsSqlServer<string>();

        Assert.Null(result.Data);
        Assert.Null(result.Exc);
    }

    [Fact]
    public void ResultWithListData_ReturnsCorrectList()
    {
        var list = new List<int> { 1, 2, 3 };
        var result = new ResultWithExceptionMsSqlServer<List<int>>(list);

        Assert.NotNull(result.Data);
        Assert.Equal(3, result.Data!.Count);
        Assert.Equal(1, result.Data[0]);
        Assert.Equal(2, result.Data[1]);
        Assert.Equal(3, result.Data[2]);
        Assert.Null(result.Exc);
    }
}
