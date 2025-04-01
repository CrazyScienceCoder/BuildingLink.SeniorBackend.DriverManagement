using BuildingLink.DriverManagement.Domain.Drivers;
using BuildingLink.DriverManagement.Domain.Types;

namespace BuildingLink.DriverManagement.Domain;

public interface IRepository<in TEntity>
{
    Task<Id> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<Driver?> GetAsync(Id id, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Id id, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Id id, CancellationToken cancellationToken);
}