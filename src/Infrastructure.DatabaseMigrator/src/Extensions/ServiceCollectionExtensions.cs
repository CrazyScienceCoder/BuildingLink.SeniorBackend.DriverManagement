using Microsoft.Extensions.DependencyInjection;

namespace BuildingLink.DriverManagement.Infrastructure.Migrator.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMigrator(this IServiceCollection services)
    {
        services.AddTransient<IMigrator, Migrator>();
    }
}
