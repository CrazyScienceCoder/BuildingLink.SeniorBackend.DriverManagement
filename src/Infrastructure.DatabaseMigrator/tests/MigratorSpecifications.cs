using Dapper;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Infrastructure.Migrator.Tests;

public partial class MigratorSpecifications
{
    [Fact]
    public void Migrator_AttemptToCreateObject_ShouldSucceed()
    {
        var testBuilder = new TestBuilder();

        var migrator = testBuilder.Build();
        migrator.Should().NotBeNull();
    }

    [Fact]
    public async Task Migrate_Run_ShouldShouldApplyMigrations()
    {
        var testBuilder = new TestBuilder();

        var migrator = testBuilder.Build();

        migrator.Migrate();

        var expectedMigrations = TestBuilder.MigrationList.Select(migration =>
            $"SELECT COUNT(1) FROM SchemaVersions WHERE ScriptName LIKE '%{migration}';");

        foreach (var sql in expectedMigrations)
        {
            var migrationCount = await ConnectionFactory.Connection.ExecuteScalarAsync<int>(sql);

            migrationCount.Should().Be(1);
        }
    }
}