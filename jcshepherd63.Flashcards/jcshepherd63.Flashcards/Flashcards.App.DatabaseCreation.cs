using Microsoft.Extensions.Configuration;

namespace DatabaseCreation;

internal class DatabaseSetup
{
    public static string GetDbConnectionString()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("DefaultConnection");
        return connectionString;
    }
}
