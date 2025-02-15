using SocialService.Exceptions;
using SocialService.Post.Common.Repository;

namespace SocialService.Post.UploadPostImages;

/// <summary>
///     Handler para o comando de upload de imagens de um post.
/// </summary>
/// <param name="repository"></param>
/// <param name="blobStorageProvider"></param>
public class UploadPostImageCommandHandler(IPostGraphRepository repository, IBlobStorageProvider blobStorageProvider)
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
        List<string> images = new();

        var containerName = $"{ProfileContext.ProfileId}";

        foreach (var image in command.Images)
        {
            var blobName = $"post-images/{command.PostId}/{Guid.NewGuid()}.{image.imageName.Split('.').Last()}";

            await blobStorageProvider.WriteBlobAsync(image.fileContent, blobName, containerName);
            
            images.Add(blobName);
        }

        await repository.UploadPostImagesAsync(command.PostId, images);

        return true;
    }
}