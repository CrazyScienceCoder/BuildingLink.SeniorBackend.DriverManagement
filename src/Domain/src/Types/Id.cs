using Ardalis.GuardClauses;
using BuildingLink.DriverManagement.Domain.Exceptions;

namespace BuildingLink.DriverManagement.Domain.Types;

public sealed record Id
{
    public int Value { get; init; }

    public Id(int id)
    {
        Guard.Against.Negative(id, nameof(id),
            exceptionCreator: () => new InvalidIdException("Id cannot be negative.", nameof(id)));

        Value = id;
    }

    public static implicit operator int(Id id)
        => id.Value;

    public static implicit operator Id(int id)
        => new(id: id);
}