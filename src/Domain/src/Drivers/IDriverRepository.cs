namespace BuildingLink.DriverManagement.Domain.Drivers;

public interface IDriverRepository : IRepository<Driver>
{
    Task<IReadOnlyList<Driver>> GetAlphabetizedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}