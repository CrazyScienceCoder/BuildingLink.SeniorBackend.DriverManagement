using System.Reflection;

namespace BuildingLink.DriverManagement.Infrastructure.Sql;

public static class SqlQueryManager
{
    public static async Task<string> GetTextAsync(string sqlFileName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = $"{typeof(SqlQueryManager).Namespace}.Queries.{sqlFileName}.sql";

        await using var stream = assembly.GetManifestResourceStream(resourceName)!;
        using var reader = new StreamReader(stream);
        var content = await reader.ReadToEndAsync();

        return content;
    }
}