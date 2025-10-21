// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoMsSqlServer.Helpers;

public static class IdentityHelpers
{
    public static Task EnableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: true);
    public static Task DisableIdentityInsert<T>(this DbContext context) => SetIdentityInsert<T>(context, enable: false);
    private static Task SetIdentityInsert<T>(DbContext context, bool enable)
    {
        var entityType = context.Model.FindEntityType(typeof(T));
        var value = enable ? "ON" : "OFF";
        return context.Database.ExecuteSqlRawAsync(
            $"SET IDENTITY_INSERT {entityType.GetSchema()}.{entityType.GetTableName()} {value}");
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