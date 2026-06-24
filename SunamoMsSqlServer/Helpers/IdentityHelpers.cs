namespace SunamoMsSqlServer.Helpers;

public static class IdentityHelpers
{
    public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, isEnabled: true);

    public static Task DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, isEnabled: false);

    private static Task SetIdentityInsert<T>(DbContext context, bool isEnabled)
    {
        var entityType = context.Model.FindEntityType(typeof(T))
            ?? throw new InvalidOperationException($"Entity type {typeof(T).Name} not found in the model.");
        var onOffValue = isEnabled ? "ON" : "OFF";
        return context.Database.ExecuteSqlRawAsync(
            "SET IDENTITY_INSERT " + entityType.GetSchema() + "." + entityType.GetTableName() + " " + onOffValue);
    }

    [Obsolete("You may need to modify the method so that async methods run correctly.")]
    public static void SaveChangesWithIdentityInsert<T>(this DbContext context)
    {
        using var transaction = context.Database.BeginTransaction();
        context.EnableIdentityInsert<T>();
        context.SaveChanges();
        context.DisableIdentityInsert<T>();
        transaction.Commit();
    }

    public static async Task SaveChangesWithIdentityInsertAsync<T>(this DbContext context)
    {
        using var transaction = context.Database.BeginTransaction();
        await context.EnableIdentityInsert<T>();
        await context.SaveChangesAsync();
        await context.DisableIdentityInsert<T>();
        await transaction.CommitAsync();
    }
}
