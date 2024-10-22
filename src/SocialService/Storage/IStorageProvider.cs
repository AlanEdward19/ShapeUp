namespace SocialService.Storage;

public interface IStorageProvider
{
    Task<byte[]> ReadBlobAsync(string blobName, string containerName);
    Task WriteBlobAsync(MemoryStream data, string blobName, string containerName);
    Task DeleteBlobAsync(string blobName, string containerName);
}