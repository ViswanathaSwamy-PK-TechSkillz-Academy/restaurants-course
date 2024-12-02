using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Storage;

internal class BlobStorageService : IBlobStorageService
{
    public Task<string> UploadToBlobAsync(Stream data, string fileName)
    {
        throw new NotImplementedException();
    }
}
