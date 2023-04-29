using Microsoft.EntityFrameworkCore;
using Ocs.Database.Context;

namespace Ocs.Api.Extensions;

public static class DatabaseMigrateExtension
{
    public static void DatabaseMigrate(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<OcsContext>();

        if (context == null)
            throw new InvalidOperationException("Database context is NULL at Migrator service.");

        context.Database.Migrate();
    }
}