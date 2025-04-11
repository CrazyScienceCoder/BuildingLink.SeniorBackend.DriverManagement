using BuildingLink.DriverManagement.Domain.Types;

namespace BuildingLink.DriverManagement.Domain;

public interface IRepository<TEntity>
{
    Task<Id> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<TEntity?> GetAsync(Id id, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Id id, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Id id, CancellationToken cancellationToken);
}