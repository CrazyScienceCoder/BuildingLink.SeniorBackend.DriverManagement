using System.Data;
using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Types;
using BuildingLink.DriverManagement.Infrastructure.Constants;
using BuildingLink.DriverManagement.Infrastructure.Drivers.Mappers;
using BuildingLink.DriverManagement.Infrastructure.Sql;
using Dapper;

namespace BuildingLink.DriverManagement.Infrastructure.Drivers;

public class DriverRepository(IDbConnection connection) : IDriverRepository
{
    private const int NumberOfUnacceptableAffectedRows = 0;

    public async Task<Id> CreateAsync(Driver entity, CancellationToken cancellationToken)
    {
        var sql = await SqlQueryManager.GetTextAsync(SqlFiles.InsertDriver);

        var commandDefinition = new CommandDefinition(commandText: sql
            , parameters: entity.ToDriverModel()
            , cancellationToken: cancellationToken);

        return await connection.ExecuteScalarAsync<int>(commandDefinition);
    }

    public async Task<Driver?> GetAsync(Id id, CancellationToken cancellationToken)
    {
        var sql = await SqlQueryManager.GetTextAsync(SqlFiles.SelectDriverById);

        var parameters = new
        {
            Id = id.Value
        };

        var commandDefinition = new CommandDefinition(commandText: sql
            , parameters: parameters
            , cancellationToken: cancellationToken);

        var driver = await connection.QueryFirstOrDefaultAsync<Models.Driver>(commandDefinition);

        return driver?.ToDomainDriver();
    }

    public async Task<bool> UpdateAsync(Driver entity, CancellationToken cancellationToken)
    {
        var sql = await SqlQueryManager.GetTextAsync(SqlFiles.UpdateDriver);

        var commandDefinition = new CommandDefinition(commandText: sql
            , parameters: entity.ToDriverModel()
            , cancellationToken: cancellationToken);

        var affectedRows = await connection.ExecuteAsync(commandDefinition);

        return affectedRows > NumberOfUnacceptableAffectedRows;
    }

    public async Task<bool> DeleteAsync(Id id, CancellationToken cancellationToken)
    {
        var sql = await SqlQueryManager.GetTextAsync(SqlFiles.DeleteDriverById);

        var parameters = new
        {
            Id = id.Value
        };

        var commandDefinition = new CommandDefinition(commandText: sql
            , parameters: parameters
            , cancellationToken: cancellationToken);

        var affectedRows = await connection.ExecuteAsync(commandDefinition);

        return affectedRows > NumberOfUnacceptableAffectedRows;
    }

    public async Task<bool> ExistsAsync(Id id, CancellationToken cancellationToken)
    {
        var sql = await SqlQueryManager.GetTextAsync(SqlFiles.DriverExistsById);

        var parameters = new
        {
            Id = id.Value
        };

        var commandDefinition = new CommandDefinition(commandText: sql
            , parameters: parameters
            , cancellationToken: cancellationToken);

        return await connection.ExecuteScalarAsync<bool>(commandDefinition);
    }

    public async Task<IReadOnlyList<Driver>> GetAlphabetizedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        if (pageSize <= 0)
        {
            return [];
        }

        var sql = await SqlQueryManager.GetTextAsync(SqlFiles.SelectNameSortedDrivers);

        var offset = (pageNumber - 1) * pageSize;

        var parameters = new
        {
            PageSize = pageSize,
            Offset = offset
        };

        var commandDefinition = new CommandDefinition(commandText: sql
            , parameters: parameters
            , cancellationToken: cancellationToken);

        var drivers = await connection.QueryAsync<Models.Driver>(commandDefinition);

        return drivers.ToDomainDrivers().ToList();
    }
}