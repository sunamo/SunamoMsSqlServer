namespace SunamoMsSqlServer._sunamo;

/// <summary>
/// Provides helper methods for throwing standard exceptions.
/// </summary>
internal class ThrowEx
{
    /// <summary>
    /// Throws an exception indicating that the method is not yet implemented.
    /// </summary>
    internal static void NotImplementedMethod()
    {
        throw new Exception("Method is not implemented");
    }
}
