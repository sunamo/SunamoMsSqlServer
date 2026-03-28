namespace SunamoMsSqlServer.Helpers;

/// <summary>
/// Provides extension methods for managing SQL Server IDENTITY_INSERT on Entity Framework Core DbContext.
/// </summary>
public static class IdentityHelpers
{
    /// <summary>
    /// Enables IDENTITY_INSERT for the specified entity type.
    /// </summary>
    /// <typeparam name="T">The entity type mapped to the target table.</typeparam>
    /// <param name="context">The DbContext instance used to execute the command.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, isEnabled: true);

    /// <summary>
    /// Disables IDENTITY_INSERT for the specified entity type.
    /// </summary>
    /// <typeparam name="T">The entity type mapped to the target table.</typeparam>
    /// <param name="context">The DbContext instance used to execute the command.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static Task DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, isEnabled: false);

    private static Task SetIdentityInsert<T>(DbContext context, bool isEnabled)
    {
        var entityType = context.Model.FindEntityType(typeof(T))
            ?? throw new InvalidOperationException($"Entity type {typeof(T).Name} not found in the model.");
        var onOffValue = isEnabled ? "ON" : "OFF";
        return context.Database.ExecuteSqlRawAsync(
            "SET IDENTITY_INSERT " + entityType.GetSchema() + "." + entityType.GetTableName() + " " + onOffValue);
    }

    /// <summary>
    /// Saves changes with IDENTITY_INSERT enabled for the specified entity type within a transaction.
    /// </summary>
    /// <typeparam name="T">The entity type mapped to the target table.</typeparam>
    /// <param name="context">The DbContext instance used to execute the command.</param>
    [Obsolete("You may need to modify the method so that async methods run correctly.")]
    public static void SaveChangesWithIdentityInsert<T>(this DbContext context)
    {
        using var transaction = context.Database.BeginTransaction();
        context.EnableIdentityInsert<T>();
        context.SaveChanges();
        context.DisableIdentityInsert<T>();
        transaction.Commit();
    }

    /// <summary>
    /// Asynchronously saves changes with IDENTITY_INSERT enabled for the specified entity type within a transaction.
    /// </summary>
    /// <typeparam name="T">The entity type mapped to the target table.</typeparam>
    /// <param name="context">The DbContext instance used to execute the command.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public static async Task SaveChangesWithIdentityInsertAsync<T>(this DbContext context)
    {
        using var transaction = context.Database.BeginTransaction();
        await context.EnableIdentityInsert<T>();
        await context.SaveChangesAsync();
        await context.DisableIdentityInsert<T>();
        await transaction.CommitAsync();
    }
}
