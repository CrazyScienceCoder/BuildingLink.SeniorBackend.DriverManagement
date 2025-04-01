
using System.Data;
using Microsoft.Data.Sqlite;

namespace BuildingLink.DriverManagement.Infrastructure.Tests;

public static class ConnectionFactory
{
    private const string InMemoryConnectionString = "Data Source=:memory:;Mode=Memory;Cache=Shared";

    public static IDbConnection InMemoryConnection
    {
        get
        {
            SQLitePCL.Batteries.Init();
            var connection = new SqliteConnection(InMemoryConnectionString);
            connection.Open();
            return connection;
        }
    }
}