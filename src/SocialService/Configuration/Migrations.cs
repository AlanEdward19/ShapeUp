using Microsoft.EntityFrameworkCore;
using SocialService.Database.Sql;

namespace SocialService.Configuration;

/// <summary>
///     Classe para atualização de migrações
/// </summary>
public static class Migrations
{
    /// <summary>
    ///     Método para atualizar as migrações
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static IApplicationBuilder UpdateMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

        var context = serviceScope?.ServiceProvider.GetRequiredService<DatabaseContext>();

        if (context == null)
            return app;

        try
        {
            var pendingMigrations = context.Database.GetPendingMigrations();

            if (pendingMigrations.Any())
                context.Database.Migrate();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return app;
    }
}