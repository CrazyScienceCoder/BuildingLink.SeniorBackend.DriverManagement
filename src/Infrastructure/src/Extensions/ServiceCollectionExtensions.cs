using System.Data;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Infrastructure.Configurations;
using BuildingLink.DriverManagement.Infrastructure.Drivers;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BuildingLink.DriverManagement.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<ConnectionStrings>(
            options => configuration.GetSection(nameof(ConnectionStrings)).Bind(options));

        services.AddTransient<IDbConnection>(provider =>
        {
            var options = provider.GetRequiredService<IOptions<ConnectionStrings>>().Value;

            SQLitePCL.Batteries.Init();
            var connection = new SqliteConnection(options.DriversDatabase);
            connection.Open();
            return connection;
        });

        services.AddTransient<IDriverRepository, DriverRepository>();
    }
}
