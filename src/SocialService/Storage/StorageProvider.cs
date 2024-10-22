using Azure.Storage.Blobs;

namespace SocialService.Storage;

public class StorageProvider(string connectionString, ILogger<StorageProvider> logger) : IStorageProvider
{
    private readonly BlobServiceClient _blobServiceClient = new(connectionString);

    public async Task<byte[]> ReadBlobAsync(string blobName, string containerName)
    {
        logger.LogInformation("Retornando conteudo do blob");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        if (!await containerClient.ExistsAsync())
            throw new Exception("Container não existe");

        var blobClient = containerClient.GetBlobClient(blobName);

        try
        {
            var response = await blobClient.DownloadAsync();
            using (var streamReader = new MemoryStream())
            {
                await response.Value.Content.CopyToAsync(streamReader);
                return streamReader.ToArray();
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task WriteBlobAsync(MemoryStream data, string blobName, string containerName)
    {
        logger.LogInformation("Escrevendo conteudo no blob");
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(blobName);

        data.Position = 0;

        await blobClient.UploadAsync(data, true);
    }

    public async Task DeleteBlobAsync(string blobName, string containerName)
    {
        logger.LogInformation("Deletando blob");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        if (!await containerClient.ExistsAsync())
            throw new Exception("Container não existe");

        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync();
    }
}