using System.Reflection;
using BuildingLink.DriverManagement.Infrastructure.Configurations;
using DbUp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BuildingLink.DriverManagement.Infrastructure.Migrator;

public class Migrator(IOptions<ConnectionStrings> options, ILogger<Migrator> logger) : IMigrator
{
    private readonly ConnectionStrings _options = options.Value;

    public void Migrate()
    {
        logger.LogInformation("Starting database migration...");

        var upgradeEngine = DeployChanges.To
            .SqliteDatabase(_options.DriversDatabase)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogTo(logger)
            .Build();

        var result = upgradeEngine.PerformUpgrade();

        if (!result.Successful)
        {
            logger.LogError("Database migration failed: {Error}", result.Error);
            throw new Exception("Database migration failed", result.Error);
        }

        logger.LogInformation("Database migration completed successfully.");
    }
}