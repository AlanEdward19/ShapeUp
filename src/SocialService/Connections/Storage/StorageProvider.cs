using System.Text.RegularExpressions;
using Azure.Storage.Blobs;

namespace SocialService.Storage;

/// <summary>
/// Provedor de armazenamento de arquivos
/// </summary>
/// <param name="connectionString"></param>
/// <param name="logger"></param>
public class StorageProvider(string connectionString, ILogger<StorageProvider> logger) : IStorageProvider
{
    private readonly BlobServiceClient _blobServiceClient = new(connectionString);

    /// <summary>
    /// Método para ler o conteúdo de um blob
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<byte[]> ReadBlobAsync(string blobName, string containerName)
    {
        blobName = SanitizeName(blobName, true);
        containerName = SanitizeName(containerName);

        logger.LogInformation("Retornando conteudo do blob");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        if (!await containerClient.ExistsAsync())
            throw new Exception("Container não existe");

        var blobClient = containerClient.GetBlobClient(blobName);

        var response = await blobClient.DownloadAsync();
        using (var streamReader = new MemoryStream())
        {
            await response.Value.Content.CopyToAsync(streamReader);
            return streamReader.ToArray();
        }
    }

    /// <summary>
    /// Método para escrever o conteúdo em um blob
    /// </summary>
    /// <param name="data"></param>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    public async Task WriteBlobAsync(MemoryStream data, string blobName, string containerName)
    {
        blobName = SanitizeName(blobName, true);
        containerName = SanitizeName(containerName);

        logger.LogInformation("Escrevendo conteudo no blob");
        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient(blobName);

        data.Position = 0;

        await blobClient.UploadAsync(data, true);
    }
    
    /// <summary>
    /// Método para criar uma pasta
    /// </summary>
    /// <param name="folderName"></param>
    /// <param name="containerName"></param>
    public async Task CreateFolderAsync(string folderName, string containerName)
    {
        folderName = SanitizeName(folderName, true);
        containerName = SanitizeName(containerName);

        logger.LogInformation("Criando pasta");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();
    }

    /// <summary>
    /// Método para deletar um blob
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteBlobAsync(string blobName, string containerName)
    {
        blobName = SanitizeName(blobName, true);
        containerName = SanitizeName(containerName);

        logger.LogInformation("Deletando blob");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        if (!await containerClient.ExistsAsync())
            throw new Exception("Container não existe");

        var blobClient = containerClient.GetBlobClient(blobName);

        await blobClient.DeleteIfExistsAsync();
    }
    
    public async Task DeleteFolderAsync(string folderName, string containerName)
    {
        folderName = SanitizeName(folderName, true);
        containerName = SanitizeName(containerName);

        logger.LogInformation("Deletando pasta");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        if (!await containerClient.ExistsAsync())
            throw new Exception("Container não existe");

        await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: folderName))
        {
            var blobClient = containerClient.GetBlobClient(blobItem.Name);
            await blobClient.DeleteIfExistsAsync();
        }
    }

    public async Task DeleteContainerAsync(string containerName)
    {
        containerName = SanitizeName(containerName);

        logger.LogInformation("Deletando container");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        await containerClient.DeleteIfExistsAsync();
    }

    /// <summary>
    /// Método para renomear um blob
    /// </summary>
    /// <param name="oldBlobName"></param>
    /// <param name="newBlobName"></param>
    /// <param name="containerName"></param>
    /// <exception cref="Exception"></exception>
    public async Task RenameBlobAsync(string oldBlobName, string newBlobName, string containerName)
    {
        oldBlobName = SanitizeName(oldBlobName);
        newBlobName = SanitizeName(newBlobName);
        containerName = SanitizeName(containerName);

        logger.LogInformation("Renomeando blob");

        var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

        if (!await containerClient.ExistsAsync())
            throw new Exception("Container não existe");

        var oldBlobClient = containerClient.GetBlobClient(oldBlobName);

        if (!await oldBlobClient.ExistsAsync())
            throw new Exception("Blob antigo não existe");

        var newBlobClient = containerClient.GetBlobClient(newBlobName);

        // Baixar o conteúdo do blob antigo
        var response = await oldBlobClient.DownloadAsync();
        using (var streamReader = new MemoryStream())
        {
            await response.Value.Content.CopyToAsync(streamReader);
            streamReader.Position = 0;

            // Criar o novo blob com o conteúdo baixado
            await newBlobClient.UploadAsync(streamReader, true);
        }

        // Deletar o blob antigo
        await oldBlobClient.DeleteIfExistsAsync();
    }

    private string SanitizeName(string name, bool allowSlashes = false)
    {
        if (allowSlashes)
            name = Regex.Replace(name, "[^a-zA-Z0-9-/.]", "_");
        else
            name = Regex.Replace(name, "[^a-zA-Z0-9-.]", "_");

        // Ensure that the blobName starts and ends with an alphanumeric character
        if (!char.IsLetterOrDigit(name[0]))
            name = "_" + name;

        if (!char.IsLetterOrDigit(name[name.Length - 1]))
            name = name + "_";

        name = name.Replace("-", "");
        name = name.Replace("_", "");

        return name;
    }
}