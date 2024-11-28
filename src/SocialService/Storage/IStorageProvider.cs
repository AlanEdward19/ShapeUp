﻿namespace SocialService.Storage;

/// <summary>
/// Interface para prover armazenamento de arquivos
/// </summary>
public interface IStorageProvider
{
    /// <summary>
    /// Método para renomear um blob
    /// </summary>
    /// <param name="oldBlobName"></param>
    /// <param name="newBlobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task RenameBlobAsync(string oldBlobName, string newBlobName, string containerName);
    
    /// <summary>
    /// Método para ler um blob
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task<byte[]> ReadBlobAsync(string blobName, string containerName);
    
    /// <summary>
    /// Método para escrever um blob
    /// </summary>
    /// <param name="data"></param>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task WriteBlobAsync(MemoryStream data, string blobName, string containerName);
    
    /// <summary>
    /// Método para deletar um blob
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task DeleteBlobAsync(string blobName, string containerName);
}