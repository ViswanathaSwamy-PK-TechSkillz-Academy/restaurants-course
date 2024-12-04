namespace Restaurants.Infrastructure.Configuration;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = string.Empty;

    public string LogosContainerName { get; set; } = string.Empty;

    public string AccountKey { get; set; } = string.Empty;
}
