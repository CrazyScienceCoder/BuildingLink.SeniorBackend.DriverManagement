using BuildingLink.DriverManagement.Infrastructure.Migrator;

namespace BuildingLink.DriverManagement.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication RunDatabaseMigrator(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var migrator = scope.ServiceProvider.GetRequiredService<IMigrator>();
        migrator.Migrate();

        return application;
    }
}