namespace SunamoMsSqlServer.Helpers;

/// <summary>
/// Represents the identity helpers type.
/// </summary>
public static class IdentityHelpers
{
    /// <summary>
    /// Enable identity insert t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="context">The context.</param>
    public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, isEnabled: true);

    /// <summary>
    /// Disable identity insert t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="context">The context.</param>
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
    /// Save changes with identity insert t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="context">The context.</param>
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
    /// Save changes with identity insert async t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="context">The context.</param>
    public static async Task SaveChangesWithIdentityInsertAsync<T>(this DbContext context)
    {
        using var transaction = context.Database.BeginTransaction();
        await context.EnableIdentityInsert<T>();
        await context.SaveChangesAsync();
        await context.DisableIdentityInsert<T>();
        await transaction.CommitAsync();
    }
}
