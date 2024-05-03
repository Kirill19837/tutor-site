using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace TutorPro.Application.Extensions;

public static class AppExtensions
{
    public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app, IConfiguration configuration)
    {
        var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        var loggerFactory = app.ApplicationServices.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(nameof(AppExtensions));

        try
        {
            var pendingMigrations = db.Database.GetPendingMigrations();
            var migrations = pendingMigrations as IList<string> ?? pendingMigrations.ToList();

            if (!migrations.Any())
            {
                logger.LogInformation("No pending migrations for database were found");
                return app;
            }

            logger.LogInformation("Pending migrations for the database were found");

            foreach (var migration in migrations)
            {
                logger.LogInformation("Pending: {Migration}", migration);
            }

            logger.LogInformation("Migrations are going to be applied");
            db.Database.Migrate();
            logger.LogInformation("Pending migrations were applied successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{ErrorMessage}", ex.Message);
        }

        return app;
    }
}