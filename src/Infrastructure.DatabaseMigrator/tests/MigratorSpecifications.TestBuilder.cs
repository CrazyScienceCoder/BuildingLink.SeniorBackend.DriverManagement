using BuildingLink.DriverManagement.Infrastructure.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace BuildingLink.DriverManagement.Infrastructure.Migrator.Tests;

public partial class MigratorSpecifications
{
    private class TestBuilder
    {
        private readonly IOptions<ConnectionStrings> _options = Options.Create(new ConnectionStrings
        {
            DriversDatabase = ConnectionFactory.ConnectionString
        });

        private readonly Mock<ILogger<Migrator>> _loggerMock = new();

        public static readonly List<string> MigrationList =
        [
            "0000_CreateDriversTable.sql",
            "0001_SeedTenDrivers.sql"
        ];

        public Migrator Build()
        {
            return new Migrator(_options, _loggerMock.Object);
        }
    }
}