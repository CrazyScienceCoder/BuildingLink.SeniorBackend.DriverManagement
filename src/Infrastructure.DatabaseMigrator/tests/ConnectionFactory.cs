using System.Data;
using Microsoft.Data.Sqlite;

namespace BuildingLink.DriverManagement.Infrastructure.Migrator.Tests;

public static class ConnectionFactory
{
    public const string ConnectionString = "Data Source=TestDatabase.db;";

    public static IDbConnection Connection
    {
        get
        {
            SQLitePCL.Batteries.Init();
            var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}