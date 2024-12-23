using SocialService.Common;
using SocialService.Common.Interfaces;
using SocialService.Connections.Storage;
using SocialService.Exceptions;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.UploadPostImages;

/// <summary>
///     Handler para o comando de upload de imagens de um post.
/// </summary>
/// <param name="repository"></param>
/// <param name="storageProvider"></param>
public class UploadPostImageCommandHandler(IPostGraphRepository repository, IStorageProvider storageProvider)
    : IHandler<bool, UploadPostImageCommand>
{
    /// <summary>
    ///     Método para lidar com o comando de upload de imagens de um post.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<bool> HandleAsync(UploadPostImageCommand command, CancellationToken cancellationToken)
    {
        List<Guid> images = new();

        var containerName = $"{ProfileContext.ProfileId}";

        foreach (var image in command.Images)
        {
            Guid id = Guid.NewGuid();
            var blobName = $"post-images/{command.PostId}/{id}.{image.imageName.Split('.').Last()}";

            await storageProvider.WriteBlobAsync(image.fileContent, blobName, containerName);
            images.Add(id);
        }

        await repository.UploadPostImagesAsync(command.PostId, images);

        return true;
    }
}