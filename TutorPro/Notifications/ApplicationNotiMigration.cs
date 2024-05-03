using Microsoft.EntityFrameworkCore;
using TutorPro.Application;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace TutorPro.Notifications
{
    public class ApplicationNotiMigration(ApplicationDbContext dbContext, ILogger<ApplicationNotiMigration> logger) : INotificationAsyncHandler<UmbracoApplicationStartedNotification>
    {
        public async Task HandleAsync(UmbracoApplicationStartedNotification notification, CancellationToken cancellationToken)
        {
            IEnumerable<string> pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync(cancellationToken);
            try
            {
                var migrations = pendingMigrations as IList<string> ?? pendingMigrations.ToList();

                if (!migrations.Any())
                {
                    logger.LogInformation("No pending migrations for database were found");
                    await Task.CompletedTask;
                }

                logger.LogInformation("Pending migrations for the database were found");

                foreach (var migration in migrations)
                {
                    logger.LogInformation("Pending: {Migration}", migration);
                }

                logger.LogInformation("Migrations are going to be applied");
                await dbContext.Database.MigrateAsync(cancellationToken);
                logger.LogInformation("Pending migrations were applied successfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{ErrorMessage}", ex.Message);
            }
        }
    }
}
