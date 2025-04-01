using System.Data;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Infrastructure.Constants;
using BuildingLink.DriverManagement.Infrastructure.Drivers;
using BuildingLink.DriverManagement.Infrastructure.Drivers.Mappers;
using BuildingLink.DriverManagement.Infrastructure.Sql;
using Dapper;

namespace BuildingLink.DriverManagement.Infrastructure.Tests.Drivers;

public partial class DriverRepositorySpecifications
{
    private class TestBuilder(IDbConnection connection)
    {
        public static readonly List<Driver> NewDrivers =
        [
            new()
            {
                Id = 0,
                Email = "test@domain.com",
                FirstName = "Oliver",
                LastName = "Johnson",
                PhoneNumber = "+1-404-724-1937"
            },

            new()
            {
                Id = 0,
                Email = "test2@domain.com",
                FirstName = "Dcba",
                LastName = "Fehg",
                PhoneNumber = "+1-404-724-1938"
            },
            new()
            {
                Id = 0,
                Email = "test3@domain.com",
                FirstName = "Andy",
                LastName = "Johnson",
                PhoneNumber = "+1-404-724-1939"
            },

            new()
            {
                Id = 0,
                Email = "test4@domain.com",
                FirstName = "Andy",
                LastName = "Cruise",
                PhoneNumber = "+1-404-724-1948"
            }
        ];

        public async Task EnsureTableCreatedAsync()
        {
            const string createTableSql = """
                                          CREATE TABLE IF NOT EXISTS Drivers (
                                              Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                              FirstName TEXT NOT NULL CHECK(length(FirstName) <= 50),
                                              LastName TEXT NOT NULL CHECK(length(LastName) <= 50),
                                              Email TEXT NOT NULL UNIQUE CHECK(length(Email) <= 150),
                                              PhoneNumber TEXT NOT NULL CHECK(length(PhoneNumber) <= 20),
                                              CreatedAtUtc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
                                              UpdatedAtUtc DATETIME NULL
                                          );
                                          """;

            await connection.ExecuteAsync(createTableSql);
        }

        public async Task<int> InsertDriverAsync(Driver? driver = null)
        {
            var driverToInsert = driver ?? NewDrivers[0];

            var sql = await SqlQueryManager.GetTextAsync(SqlFiles.InsertDriver);

            return await connection.ExecuteScalarAsync<int>(sql, driverToInsert.ToDriverModel());
        }

        public async Task InsertDriversAsync()
        {
            foreach (var newDriver in NewDrivers)
            {
                await InsertDriverAsync(newDriver);
            }
        }

        public async Task<Driver?> GetDriverAsync(int id)
        {
            var sql = await SqlQueryManager.GetTextAsync(SqlFiles.SelectDriverById);

            var parameters = new
            {
                Id = id
            };

            var driver = await connection.QueryFirstAsync<Models.Driver>(sql, parameters);

            return driver.ToDomainDriver();
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            var sql = await SqlQueryManager.GetTextAsync(SqlFiles.SelectAllDrivers);

            var insertedDrivers = await connection.QueryAsync<Models.Driver>(sql);
            return insertedDrivers.Select(d => d.ToDomainDriver()).ToList();
        }

        public DriverRepository Build()
        {
            return new DriverRepository(connection);
        }
    }
}