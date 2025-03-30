using AuthService.Connections.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Configuration;

public static class Migration
{
    public static IApplicationBuilder UpdateMigrations(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()?.CreateScope();

        var context = serviceScope?.ServiceProvider.GetRequiredService<AuthDbContext>();

        if (context != null)
        {
            try
            {
                var pendingMigrations = context.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any())
                {
                    context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        return app;
    }
}