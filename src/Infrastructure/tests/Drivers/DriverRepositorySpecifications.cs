using BuildingLink.DriverManagement.Domain.Drivers;
using FluentAssertions;

namespace BuildingLink.DriverManagement.Infrastructure.Tests.Drivers;

public partial class DriverRepositorySpecifications
{
    [Fact]
    public void DriverRepository_AttemptToCreateObject_ShouldSucceed()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;

        var testBuilder = new TestBuilder(dbConnection);

        var repository = testBuilder.Build();

        repository.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateAsync_ValidDrivers_ShouldInsertDriversToDb()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        var driver1 = TestBuilder.NewDrivers[0];

        var driver2 = TestBuilder.NewDrivers[1];

        var repository = testBuilder.Build();

        var driver1Id = await repository.CreateAsync(driver1, CancellationToken.None);

        driver1 = driver1 with { Id = driver1Id };

        var driver2Id = await repository.CreateAsync(driver2, CancellationToken.None);

        driver2 = driver2 with { Id = driver2Id };

        var drivers = await testBuilder.GetAllDriversAsync();

        driver1Id.Value.Should().Be(1);
        driver2Id.Value.Should().Be(2);
        drivers.Should().HaveCount(2);
        drivers.Should().Contain(driver1);
        drivers.Should().Contain(driver2);
    }

    [Fact]
    public async Task GetAsync_ExistingRecord_ShouldReturnDriver()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();
        var insertedDriverId = await testBuilder.InsertDriverAsync();

        var repository = testBuilder.Build();

        var driver = await repository.GetAsync(insertedDriverId, CancellationToken.None);

        driver.Should().NotBeNull();
        driver.Id.Value.Should().Be(insertedDriverId);
    }

    [Fact]
    public async Task GetAsync_NonExistingRecord_ShouldReturnNull()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        const int nonExistingId = 999;

        var repository = testBuilder.Build();

        var driver = await repository.GetAsync(nonExistingId, CancellationToken.None);

        driver.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_ExistingRecord_ShouldUpdateDriver()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        var insertedDriverId = await testBuilder.InsertDriverAsync();

        var repository = testBuilder.Build();

        var updatedDriver = new Driver
        {
            Id = insertedDriverId,
            FirstName = "UpdatedFirstName",
            LastName = "UpdatedLastName",
            Email = "updated@domain.com",
            PhoneNumber = "+1-404-724-1939"
        };

        var result = await repository.UpdateAsync(updatedDriver, CancellationToken.None);

        var driver = await testBuilder.GetDriverAsync(updatedDriver.Id);

        result.Should().BeTrue();
        driver.Should().NotBeNull();
        driver.Id.Value.Should().Be(updatedDriver.Id.Value);
        driver.FirstName.Value.Should().Be(updatedDriver.FirstName.Value);
        driver.LastName.Value.Should().Be(updatedDriver.LastName.Value);
        driver.Email.Value.Should().Be(updatedDriver.Email.Value);
        driver.PhoneNumber.Value.Should().Be(updatedDriver.PhoneNumber.Value);
    }

    [Fact]
    public async Task UpdateAsync_ExistingRecordWithTheSameValues_ShouldReturnTrue()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        var insertedDriverId = await testBuilder.InsertDriverAsync();

        var insertedDriver = await testBuilder.GetDriverAsync(insertedDriverId);

        var repository = testBuilder.Build();

        var result = await repository.UpdateAsync(insertedDriver!, CancellationToken.None);

        var driver = await testBuilder.GetDriverAsync(insertedDriver!.Id);

        result.Should().BeTrue();
        driver.Should().NotBeNull();
        driver.Id.Value.Should().Be(insertedDriver.Id.Value);
        driver.FirstName.Value.Should().Be(insertedDriver.FirstName.Value);
        driver.LastName.Value.Should().Be(insertedDriver.LastName.Value);
        driver.Email.Value.Should().Be(insertedDriver.Email.Value);
        driver.PhoneNumber.Value.Should().Be(insertedDriver.PhoneNumber.Value);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingRecord_ShouldReturnFalse()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        const int nonExistingId = 999;

        await testBuilder.InsertDriverAsync();

        var repository = testBuilder.Build();

        var updatedDriver = TestBuilder.NewDrivers[0] with { Id = nonExistingId, FirstName = "UpdatedFirstName" };

        var result = await repository.UpdateAsync(updatedDriver, CancellationToken.None);
        result.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteAsync_ExistingRecord_ShouldDeleteDriver()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();
        var insertedDriverId = await testBuilder.InsertDriverAsync();

        var repository = testBuilder.Build();

        var result = await repository.DeleteAsync(insertedDriverId, CancellationToken.None);

        var drivers = await testBuilder.GetAllDriversAsync();

        result.Should().BeTrue();
        drivers.Should().NotContain(d => d.Id == insertedDriverId);
    }

    [Fact]
    public async Task DeleteAsync_NonExistingRecord_ShouldReturnFalse()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        const int nonExistingId = 999;

        var repository = testBuilder.Build();

        var result = await repository.DeleteAsync(nonExistingId, CancellationToken.None);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task ExistsAsync_ExistingRecord_ShouldReturnTrue()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();
        var insertedDriverId = await testBuilder.InsertDriverAsync();

        var repository = testBuilder.Build();

        var result = await repository.ExistsAsync(insertedDriverId, CancellationToken.None);

        result.Should().BeTrue();
    }

    [Fact]
    public async Task ExistsAsync_NonExistingRecord_ShouldReturnFalse()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        const int nonExistingId = 999;

        var repository = testBuilder.Build();

        var result = await repository.ExistsAsync(nonExistingId, CancellationToken.None);

        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetAlphabetizedAsync_ExistingRecords_ShouldReturnSortedDriversByName()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();
        await testBuilder.InsertDriversAsync();

        var repository = testBuilder.Build();

        var drivers = await repository.GetAlphabetizedAsync(pageNumber: 1, pageSize: 100, cancellationToken: CancellationToken.None);

        drivers.Should().HaveCount(4);
        drivers.Select(d => $"{d.FirstName.Value} {d.LastName.Value}").Should().BeInAscendingOrder();
    }

    [Fact]
    public async Task GetAlphabetizedAsync_PageNumber2PageSize2_ShouldReturnCorrectSortedCollectionOfDriversByName()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();
        await testBuilder.InsertDriversAsync();

        var expectedRecords = new[]
        {
            TestBuilder.NewDrivers[0],
            TestBuilder.NewDrivers[1]
        };

        var repository = testBuilder.Build();

        var drivers = await repository.GetAlphabetizedAsync(pageNumber: 2, pageSize: 2, cancellationToken: CancellationToken.None);

        drivers.Should().HaveCount(2);
        drivers.Select(d => $"{d.FirstName.Value} {d.LastName.Value}").Should().BeInAscendingOrder();

        foreach (var expectedRecord in expectedRecords)
        {
            drivers.Should().Contain(d => d.FirstName == expectedRecord.FirstName);
            drivers.Should().Contain(d => d.LastName == expectedRecord.LastName);
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    public async Task GetAlphabetizedAsync_PageNumberOneOrLessPageSize2_ShouldReturnTheFirstPageOfDriversSortedByName(int pageNumber)
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();
        await testBuilder.InsertDriversAsync();

        var expectedRecords = new[]
        {
            TestBuilder.NewDrivers[2],
            TestBuilder.NewDrivers[3]
        };

        var repository = testBuilder.Build();

        var drivers = await repository.GetAlphabetizedAsync(pageNumber: pageNumber, pageSize: 2, cancellationToken: CancellationToken.None);

        drivers.Should().HaveCount(2);
        drivers.Select(d => $"{d.FirstName.Value} {d.LastName.Value}").Should().BeInAscendingOrder();

        foreach (var expectedRecord in expectedRecords)
        {
            drivers.Should().Contain(d => d.FirstName == expectedRecord.FirstName);
            drivers.Should().Contain(d => d.LastName == expectedRecord.LastName);
        }
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task GetAlphabetizedAsync_PageSizeIsZeroOrLess_ShouldReturnEmptyCollection(int pageSize)
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();
        await testBuilder.InsertDriversAsync();

        var repository = testBuilder.Build();

        var drivers = await repository.GetAlphabetizedAsync(pageNumber: 1, pageSize: pageSize, cancellationToken: CancellationToken.None);

        drivers.Should().NotBeNull();
        drivers.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAsync_NonExistingRecords_ShouldReturnEmptyCollection()
    {
        using var dbConnection = ConnectionFactory.InMemoryConnection;
        var testBuilder = new TestBuilder(dbConnection);
        await testBuilder.EnsureTableCreatedAsync();

        var repository = testBuilder.Build();

        var drivers = await repository.GetAlphabetizedAsync(pageNumber: 1, pageSize: 100, cancellationToken: CancellationToken.None);

        drivers.Should().NotBeNull();
        drivers.Should().BeEmpty();
    }
}