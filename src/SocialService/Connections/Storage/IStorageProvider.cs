using SocialService.Profile.GetProfilePictures;

namespace SocialService.Connections.Storage;

/// <summary>
///     Interface para prover armazenamento de arquivos
/// </summary>
public interface IStorageProvider
{
    /// <summary>
    ///     Método para obter fotos de perfil
    /// </summary>
    /// <param name="profileId"></param>
    /// <param name="page"></param>
    /// <param name="rows"></param>
    /// <returns></returns>
    Task<IEnumerable<ProfilePicture>> GetProfilePicturesAsync(Guid profileId, int page, int rows);

    /// <summary>
    ///     Método para ler um blob
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task<byte[]> ReadBlobAsync(string blobName, string containerName);

    /// <summary>
    ///     Método para escrever um blob
    /// </summary>
    /// <param name="data"></param>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task WriteBlobAsync(MemoryStream data, string blobName, string containerName);
    
    /// <summary>
    /// Método para gerar uma URL autenticada
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    string GenerateAuthenticatedUrl(string blobName, string containerName);

    /// <summary>
    ///     Método para criar uma pasta
    /// </summary>
    /// <param name="folderName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task CreateFolderAsync(string folderName, string containerName);

    /// <summary>
    ///     Método para deletar um blob
    /// </summary>
    /// <param name="blobName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task DeleteBlobAsync(string blobName, string containerName);

    /// <summary>
    ///     Método para deletar um container
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task DeleteContainerAsync(string containerName);

    /// <summary>
    ///     Método para deletar uma pasta
    /// </summary>
    /// <param name="folderName"></param>
    /// <param name="containerName"></param>
    /// <returns></returns>
    Task DeleteFolderAsync(string folderName, string containerName);

    /// <summary>
    ///     Método para sanitizar um nome
    /// </summary>
    /// <param name="name"></param>
    /// <param name="allowSlashes"></param>
    /// <returns></returns>
    string SanitizeName(string name, bool allowSlashes = false);
}